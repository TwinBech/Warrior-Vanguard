using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
    public static int rows;
    public static int columns;
    public GameObject cellPrefab;
    public WarriorSummoner warriorSummoner;
    public Hand friendHand;
    public Hand enemyHand;
    private GridCell[,] grid;
    private List<Warrior> allWarriors = new();
    public Vector2Int? SelectedCell { get; private set; }
    private GridLayoutGroup gridLayoutGroup;
    public Transform EnemySummonerObject;
    public Image backgroundImage;

    void Start() {
        rows = Rng.Range(2, 5);
        columns = Rng.Range(9, 12);
        grid = new GridCell[columns, rows];
        GenerateGrid(FriendlySummoner.summonerData.genre);
        ClearHighlightedCells();
    }

    public float GetGridSpacingX() {
        return gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;
    }

    public float GetLeftMostGridPositionX() {
        return grid[0, 0].transform.position.x;
    }

    public float GetRightMostGridPositionX() {
        return grid[columns - 1, 0].transform.position.x;
    }

    public Vector2 GetCellDimension() {
        return new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }

    public Vector2 GetCellPosition(Vector2 gridIndex) {
        GridCell cell = grid[(int)gridIndex.x, (int)gridIndex.y];
        Vector2 pos = cell.transform.position;
        return pos;
    }

    void GenerateGrid(Genre genre) {
        RectTransform rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        float cellSpacing = 100 / columns;

        float cellWidth = (rectTransform.rect.width / columns) - cellSpacing;
        float cellHeight = (rectTransform.rect.height / rows) - cellSpacing;

        float lowestCellDimension = cellWidth < cellHeight ? cellWidth : cellHeight;
        gridLayoutGroup.cellSize = new Vector2(lowestCellDimension, lowestCellDimension);
        gridLayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);
        gridLayoutGroup.constraintCount = rows;

        backgroundImage.sprite = Resources.Load<Sprite>($"Images/Backgrounds/{genre}");

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                GameObject cell = Instantiate(
                    cellPrefab,
                    new Vector2(0, 0),
                    Quaternion.identity,
                    transform
                );
                cell.name = $"Cell[{x},{y}]";
                GridCell gridCell = cell.GetComponent<GridCell>();

                gridCell.GetComponent<RectTransform>().localScale = GetCellDimension() / gridCell.GetComponent<RectTransform>().rect.width;

                if (genre != Genre.None) {
                    int randomCell = Rng.Range(0, 12);
                    gridCell.image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cells/{genre}/{randomCell}");
                }

                gridCell.Setup(this, new Vector2(x, y));
                grid[x, y] = gridCell;
            }
        }
    }

    public async Task<bool> SelectCell(Vector2 selectedGridIndex) {
        if (!grid[(int)selectedGridIndex.x, (int)selectedGridIndex.y].IsHighlighed()) return false;

        if (warriorSummoner.getIsSummoning(Alignment.Enemy)) {
            WarriorStats luigiStats = new Luigi().GetStats();
            luigiStats.alignment = Alignment.Enemy;

            await warriorSummoner.Summon(selectedGridIndex, luigiStats, EnemySummonerObject.position);
            return true;
        }

        if (GameManager.turn == Alignment.Friend) {
            if (friendHand.selectedCard == null) return false;
            await friendHand.PlayCardFromHand(warriorSummoner, selectedGridIndex);
        } else if (GameManager.turn == Alignment.Enemy) {
            if (enemyHand.selectedCard == null) return false;
            await enemyHand.PlayCardFromHand(warriorSummoner, selectedGridIndex);
        }
        return true;
    }

    public void RegisterWarrior(Warrior warrior) {
        if (!allWarriors.Contains(warrior)) {
            allWarriors.Add(warrior);
        }
    }

    public void RemoveWarrior(Warrior warrior) {
        if (allWarriors.Contains(warrior)) {
            allWarriors.Remove(warrior);
        }
    }

    public Warrior GetCellWarrior(Vector2 gridIndex) {
        foreach (Warrior warrior in allWarriors) {
            if (warrior.gridIndex == gridIndex) {
                return warrior;
            }
        }
        return null;
    }

    public List<GridCell> GetEmptyDeploys(bool largeDeployArea, Alignment alignment) {
        List<GridCell> cells = new();
        if (alignment == Alignment.Friend) {
            for (int x = 0; x < (largeDeployArea ? Mathf.Floor(columns / 2) + FriendlySummoner.extraDeploymentArea : 3 + FriendlySummoner.extraDeploymentArea); x++) {
                for (int y = 0; y < rows; y++) {
                    if (!GetCellWarrior(new Vector2(x, y))) {
                        cells.Add(grid[x, y]);
                    }
                }
            }
        } else if (alignment == Alignment.Enemy) {
            for (int x = columns - 1; x >= (largeDeployArea ? columns - Mathf.Floor(columns / 2) : columns - 3); x--) {
                for (int y = 0; y < rows; y++) {
                    if (!GetCellWarrior(new Vector2(x, y))) {
                        cells.Add(grid[x, y]);
                    }
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomEmptyDeploy(bool largeDeployArea, Alignment alignment) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea, alignment);
        if (cells.Count == 0) return null;

        return Rng.Entry(cells);
    }

    public void HighlightDeploys(bool largeDeployArea, Alignment alignment) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea, alignment);
        for (int i = 0; i < cells.Count; i++) {
            cells[i].Highlight();
        }
    }

    public void HighlightEnemies(Alignment alignment, bool withSpell = false) {
        List<Warrior> enemies = GetEnemies(alignment);
        foreach (var enemy in enemies) {
            if (withSpell && enemy.stats.ability.spellImmunity.GetValue(enemy.stats)) continue;

            GridCell cell = grid[(int)enemy.gridIndex.x, (int)enemy.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightFriends(Alignment alignment, bool withSpell = false) {
        List<Warrior> friends = GetFriends(alignment);
        foreach (var friend in friends) {
            if (withSpell && friend.stats.ability.spellImmunity.GetValue(friend.stats)) continue;

            GridCell cell = grid[(int)friend.gridIndex.x, (int)friend.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightWarriors(bool withSpell = false) {
        List<Warrior> warriors = GetWarriors();
        foreach (var warrior in warriors) {
            if (withSpell && warrior.stats.ability.spellImmunity.GetValue(warrior.stats)) continue;

            GridCell cell = grid[(int)warrior.gridIndex.x, (int)warrior.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightAllCells() {
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                grid[x, y].Highlight();
            }
        }
    }

    public void ClearHighlightedCells() {
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                grid[x, y].ClearHighlight();
            }
        }
    }

    public List<GridCell> GetHighlighedCells() {
        List<GridCell> cells = new();
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                if (grid[x, y].IsHighlighed()) {
                    cells.Add(grid[x, y]);
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomHighlighedCell() {
        List<GridCell> cells = GetHighlighedCells();
        if (cells.Count == 0) return null;

        return Rng.Entry(cells);
    }

    public List<Warrior> GetNearbyWarriors(Vector2 gridIndex) {
        List<Warrior> warriors = new();
        for (int x = (int)gridIndex.x - 1; x <= (int)gridIndex.x + 1; x++) {
            if (x < 0 || x >= columns) continue;

            for (int y = (int)gridIndex.y - 1; y <= (int)gridIndex.y + 1; y++) {
                if (y < 0 || y >= rows) continue;
                if (gridIndex == new Vector2(x, y)) continue;

                Warrior warrior = GetCellWarrior(new Vector2(x, y));
                if (warrior != null) {
                    warriors.Add(warrior);
                }
            }
        }
        return warriors;
    }

    public List<Warrior> GetNearbyFriends(Warrior warrior) {
        List<Warrior> nearbyWarriors = GetNearbyWarriors(warrior.gridIndex);
        List<Warrior> nearbyFriends = nearbyWarriors.Where(a => a.stats.alignment == warrior.stats.alignment).ToList();

        return nearbyFriends;
    }

    public List<Warrior> GetNearbyEnemies(Warrior warrior) {
        List<Warrior> nearbyWarriors = GetNearbyWarriors(warrior.gridIndex);
        List<Warrior> nearbyEnemies = nearbyWarriors.Where(a => a.stats.alignment != warrior.stats.alignment).ToList();

        return nearbyEnemies;
    }

    public List<Warrior> GetWarriors() {
        List<Warrior> warriors = new();
        foreach (var warrior in allWarriors) {
            warriors.Add(warrior);
        }
        return warriors;
    }

    public List<Warrior> GetFriends(Alignment alignment, Warrior excludedWarrior = null) {
        List<Warrior> friends = new();
        foreach (Warrior warrior in allWarriors) {
            if (warrior.stats.alignment == alignment) {
                friends.Add(warrior);
            }
        }
        if (excludedWarrior) {
            friends.Remove(excludedWarrior);
        }
        return friends;
    }

    public List<Warrior> GetEnemies(Alignment alignment) {
        List<Warrior> enemies = new();
        foreach (Warrior warrior in allWarriors) {
            if (warrior.stats.alignment != alignment) {
                enemies.Add(warrior);
            }
        }
        return enemies;
    }

    public List<Warrior> GetDamagedFriends(Alignment alignment) {
        List<Warrior> friends = GetFriends(alignment);
        List<Warrior> damagedfriends = friends.Where(friend => friend.stats.GetHealthCurrent() < friend.stats.GetHealthMax()).ToList();
        return damagedfriends;
    }

    public int GetDistanceBetweenWarriors(Warrior warrior1, Warrior warrior2) {
        if (warrior1.gridIndex.y != warrior2.gridIndex.y) return -1;

        float dist = Mathf.Abs(warrior1.gridIndex.x - warrior2.gridIndex.x);
        return (int)dist;
    }

    public Warrior GetWarriorBehindTarget(Warrior target) {
        Warrior warrior = null;
        if (target.stats.alignment == Alignment.Enemy) {
            warrior = GetCellWarrior(target.gridIndex + new Vector2(1, 0));
        } else if (target.stats.alignment == Alignment.Friend) {
            warrior = GetCellWarrior(target.gridIndex - new Vector2(1, 0));
        }

        return warrior;
    }

    public List<Warrior> GetEnemiesInRange(Vector2 gridIndex) {
        Warrior dealer = GetCellWarrior(gridIndex);
        List<Warrior> enemiesInRange = new();
        int xIncrement = dealer.stats.alignment == Alignment.Friend
            ? 1
            : dealer.stats.alignment == Alignment.Enemy
            ? -1
            : 0;

        for (int i = 1; i <= dealer.stats.range; i++) {
            Warrior warrior = GetCellWarrior(new Vector2(gridIndex.x + (xIncrement * i), gridIndex.y));
            if (warrior && warrior.stats.alignment != dealer.stats.alignment) {
                enemiesInRange.Add(warrior);
            }
        }

        return enemiesInRange;
    }

    public Warrior GetRandomEnemy(Warrior dealer) {
        List<Warrior> enemies = GetEnemies(dealer.stats.alignment);

        Warrior enemy = Rng.Entry(enemies);
        return enemy;
    }

    public List<Warrior> GetFriendsOnColumn(Warrior warrior) {
        List<Warrior> friends = new();

        for (int y = 0; y <= rows; y++) {
            if (y == warrior.gridIndex.y) continue;

            Warrior friend = GetCellWarrior(new Vector2(warrior.gridIndex.x, y));
            if (friend != null && friend.stats.alignment == warrior.stats.alignment) {
                friends.Add(friend);
            }
        }

        return friends;
    }
}
