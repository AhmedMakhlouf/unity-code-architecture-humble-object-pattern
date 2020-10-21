using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    int Score 
    { 
        get { return score; } 
        set 
        {
            score = value;
            ui.Score = score;
        } 
    }

    int Lives 
    {
        get { return lives; }
        set
        {
            lives = value;
            ui.Lives = lives;
        }
    }

    private int lives = 3;
    private int level = 0;
    private int score = 0;
    private float timer = 0.0f;
    private int astroidsCount = 0;
    private bool playing = false;

    IUIView ui;

    private GameSettings settings;
    private IView playerShipView;

    public Game(IUIView view)
    {
        settings = Resources.Load<GameSettings>("Settings/Game");

        ui = view;
        ui.OnStartGame += OnStartGame;
        ui.OnUpdate += OnUpdate;
    }

    private void OnStartGame(object sender, System.EventArgs e)
    {
        Lives = 3;

        ui.GamePlay();
        SpawnPlayerShip();

        int spawnAstroidsCount = settings.astroidsStartCount + level * settings.addedAstroidsPerLevel;
        for (int i = 0; i < spawnAstroidsCount; i++)
        {
            var screenPosition = settings.spawnPositions[i % settings.spawnPositions.Length];
            var position = Camera.main.ViewportToWorldPoint(new Vector3(screenPosition.x, screenPosition.y));

            position.z = 0.0f;
            var direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

            SpawnAstroid(position, direction, AstroidSize.Large);
        }

        playing = true;
    }

    private void GameOver()
    {
        //TODO: Rearrange the game to start again, don't reload the scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnUpdate(object sender, UpdateEventArgs e)
    {
        if (playing == false)
            return;

        timer += e.DeltaTime;

        if (Lives == 0)
            GameOver();

        if (astroidsCount == 0)
            LevelUp();

        if (timer >= settings.spawnEnemyShipTimer)
        {
            SpawnEnemy();
            timer = 0.0f;
        }
    }

    private void LevelUp()
    {
        level++;

        int spawnAstroidsCount = settings.astroidsStartCount + level * settings.addedAstroidsPerLevel;
        for (int i = 0; i < spawnAstroidsCount; i++)
        {
            var screenPosition = settings.spawnPositions[i % settings.spawnPositions.Length];
            var position = Camera.main.ViewportToWorldPoint(new Vector3(screenPosition.x, screenPosition.y));
            
            position.z = 0.0f;
            var direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

            SpawnAstroid(position, direction, AstroidSize.Large);
        }
    }

    private void SpawnPlayerShip()
    {
        // Player Ship
        var shipPrefab = Resources.Load<GameObject>("Prefabs/PlayerShip");
        var shipInstance = Object.Instantiate(shipPrefab);
        var shipView = shipInstance.GetComponent<View>();
        var shipInput = shipInstance.GetComponent<InputReader>();
        var shield = shipInstance.GetComponent<ShieldView>();
        IPlayerShip ship = new PlayerShip(shipView, shield, shipInput);
        ship.OnHit += PlayerHit;
        ship.Shield = true;

        playerShipView = shipView;
    }

    private void SpawnEnemy()
    {
        var screenPath = settings.enemyPaths[0];

        var path = new Vector3[screenPath.points.Length];

        for(int i = 0; i < path.Length; i++)
        {
            var screenPosition = screenPath.points[i];
            var position = Camera.main.ViewportToWorldPoint(new Vector3(screenPosition.x, screenPosition.y));
            position.z = 0.0f;

            path[i] = position;
        }

        // Enemy Ship
        var enemyPrefab = Resources.Load<GameObject>("Prefabs/EnemyShip");
        var enemyInstance = Object.Instantiate(enemyPrefab, path[0], Quaternion.identity);
        var enemyView = enemyInstance.GetComponent<View>();
        IEnemyShip enemy = new EnemyShip(enemyView, playerShipView, path);

        enemy.OnHit += EnemyHit;
    }

    private void SpawnAstroid(Vector3 position, Vector3 velocity, AstroidSize size)
    {
        // Astroid 
        var astroidPrefab = Resources.Load<GameObject>("Prefabs/Astroid");
        var astroidInstance = Object.Instantiate(astroidPrefab, position, Quaternion.identity);
        var astroidView = astroidInstance.GetComponent<View>();
        var movementSettings = Resources.Load<MovementSettings>("Settings/Astroid");
        IProjectile projectile = new Projectile(astroidView, movementSettings);
        projectile.Type = ProjectileType.Astroid;
        projectile.Movement.Position = position;
        projectile.Movement.Velocity = velocity;
        
        IAstroid astroid = new Astroid(astroidView, projectile);
        
        astroid.Size = size;
        astroid.OnHit += AstroidHit;

        astroidsCount++;
    }

    private void PlayerHit(object sender, PlayerShipHitEventArgs e)
    {
        if (e.OtherProjectile == ProjectileType.PlayerBullet)
            return;

        Lives--;

        e.PlayerShip.Ship.Projectile.Movement.Position = Vector3.zero;
        e.PlayerShip.Ship.Projectile.Movement.Velocity = Vector3.zero;
        e.PlayerShip.Ship.Projectile.Movement.LookDirection = Vector3.up;
        e.PlayerShip.Shield = true;
    }

    private void EnemyHit(object sender, EnemyShipHitEventArgs e)
    {
        e.EnemyShip.Dispose();

        if (e.OtherProjectile == ProjectileType.Astroid ||
            e.OtherProjectile == ProjectileType.EnemyShip||
            e.OtherProjectile == ProjectileType.EnemyShip)
            return;

        Score += settings.enemyShipScore;
    }

    private void AstroidHit(object sender, AstroidHitEventArgs e)
    {
        if (e.OtherProjectile == ProjectileType.Astroid)
            return;

        if (e.OtherProjectile == ProjectileType.PlayerShip ||
            e.OtherProjectile == ProjectileType.PlayerBullet)
        {
            Score += settings.astroidScores[(int)e.Astroid.Size];
        }            

        var astroid = e.Astroid;
        if (astroid.Size == AstroidSize.Small)
        {
            astroid.Dispose();
            astroidsCount--;
        }
        else
        {
            var nextSize = (astroid.Size == AstroidSize.Large) ? AstroidSize.Medium : AstroidSize.Small;
            astroid.Size = nextSize;
            
            SpawnAstroid(astroid.Projectile.Movement.Position, new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)), nextSize);
            astroid.Projectile.Movement.Velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }
}
