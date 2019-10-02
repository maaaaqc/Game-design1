using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_maze : MonoBehaviour
{
	public int rows = 8;
	public int cols = 8;
    private int time = 0;
	public GameObject wall;
	public GameObject floor;
    public GameObject light;
    public GameObject kid;
    public GameObject message;
    private maze_cell[,] grid;
    private LinkedList<maze_cell> solution;

    private int cur_row = 0;
    private int cur_col = 0;
    private bool finished = false;
    private int length = 1000;

    // Start is called before the first frame update
    void Start()
    {
        create_grid();
        while(length > 15){
            length = create_solution();
            clear_visit();
        }
        show_way();
        build_solution();
        cur_row = 0;
        cur_col = 0;
        aldous_broder();
    }

    void create_grid(){
        grid = new maze_cell[rows, cols];
        for (int i = 0; i < rows; i++){
        	for (int j = cols-1; j > -1; j--){
        		GameObject f = Instantiate(floor, new Vector3(-j+9, 0.08f , i + 22), Quaternion.identity);
                f.name = "floor_" + i + "_" + j;
                GameObject fw = Instantiate(wall, new Vector3(-j+9, 0.08f , i + 21.5f), Quaternion.identity);
                fw.name = "fwall_" + i + "_" + j;
                GameObject bw = Instantiate(wall, new Vector3(-j+9, 0.08f , i + 22.5f), Quaternion.identity);
                bw.name = "bwall_" + i + "_" + j;
                GameObject lw = Instantiate(wall, new Vector3(-j+8.5f, 0.08f , i + 22f), Quaternion.Euler(0,90,0));
                lw.name = "lwall_" + i + "_" + j;
                GameObject rw = Instantiate(wall, new Vector3(-j+9.5f, 0.08f , i + 22f), Quaternion.Euler(0,90,0));
                rw.name = "rwall_" + i + "_" + j;
                grid[i,j] = new maze_cell();
                grid[i,j].x = i;
                grid[i,j].y = j;
                grid[i,j].fw = fw;
                grid[i,j].bw = bw;
                grid[i,j].lw = lw;
                grid[i,j].rw = rw;
        	}
        }
        Instantiate(kid, new Vector3(9, 0.08f , 29), Quaternion.identity);
    }

    int create_solution(){
        int cur_x = 0;
        int cur_y = 0;
        int length = 0;
        solution = new LinkedList<maze_cell>();;
        while (cur_x != 7 || cur_y != 0){
            bool moved = false;
            length++;
            if (length > 15){
                return length;
            }
            solution.AddLast(grid[cur_x, cur_y]);
            grid[cur_x, cur_y].visited = true;
            while (!moved){
                int direction = Random.Range(0,4);
                if (direction == 0){
                    if (unvisited_cell(cur_x-1, cur_y)){
                        cur_x--;
                        moved = true;
                    }
                }
                else if (direction == 1){
                    if (unvisited_cell(cur_x+1, cur_y)){
                        cur_x++;
                        moved = true;
                    }
                }
                else if (direction == 2){
                    if (unvisited_cell(cur_x, cur_y-1)){
                        cur_y--;
                        moved = true;
                    }
                }
                else {
                    if (unvisited_cell(cur_x, cur_y+1)){
                        cur_y++;
                        moved = true;
                    }
                }
            }
            if (!unvisited_cell(cur_x-1, cur_y) && !unvisited_cell(cur_x+1, cur_y) 
                && !unvisited_cell(cur_x, cur_y-1) && !unvisited_cell(cur_x, cur_y+1)){
                return 16;
            }
        }
        solution.AddLast(grid[cur_x, cur_y]);
        return length;
    }

    void build_solution(){
        if (solution.Count > 0){
            LinkedListNode<maze_cell> cur_cell = solution.First;
            grid[cur_row, cur_col].visited = true;
            cur_row = 0;
            cur_col = 0;
            for (int i = 0; i < time+2; i++){
                if (cur_cell.Next == null){
                    continue;
                }
                if (cur_cell.Next.Value.x < cur_row){
                    if (grid[cur_row, cur_col].fw){
                        Destroy(grid[cur_row, cur_col].fw);
                    }
                    cur_row--;
                    cur_cell = cur_cell.Next;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].bw){
                        Destroy(grid[cur_row, cur_col].bw);
                    }
                }
                else if (cur_cell.Next.Value.x > cur_row){
                    if (grid[cur_row, cur_col].bw){
                        Destroy(grid[cur_row, cur_col].bw);
                    }
                    cur_row++;
                    cur_cell = cur_cell.Next;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].fw){
                        Destroy(grid[cur_row, cur_col].fw);
                    }
                }
                else if (cur_cell.Next.Value.y < cur_col){
                    if (grid[cur_row, cur_col].rw){
                        Destroy(grid[cur_row, cur_col].rw);
                    }
                    cur_col--;
                    cur_cell = cur_cell.Next;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].lw){
                        Destroy(grid[cur_row, cur_col].lw);
                    }
                }
                else {
                    if (grid[cur_row, cur_col].lw){
                        Destroy(grid[cur_row, cur_col].lw);
                    }
                    cur_col++;
                    cur_cell = cur_cell.Next;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].rw){
                        Destroy(grid[cur_row, cur_col].rw);
                    }
                }
            }

        }
    }

    void show_way(){
        LinkedListNode<maze_cell> cur = solution.First;
        for (int i = 0; i < time+3; i++){
            if (cur.Next == null){
                Instantiate(light, new Vector3(-(cur.Value.y) + 9, 0.2f, cur.Value.x + 22), Quaternion.identity);
                continue;
            }
            Instantiate(light, new Vector3(-(cur.Value.y) + 9, 0.2f, cur.Value.x + 22), Quaternion.identity);
            cur = cur.Next;
        }
    }

    void clear_visit(){
        for (int i = 0; i < rows; i++){
        	for (int j = cols-1; j > -1; j--){
                grid[i,j].visited = false;
            }
        }
    }

    void aldous_broder(){
        Destroy(grid[0, 0].fw);
        grid[0, 0].visited = true;
        while (!finished){
            curser();
            restart();
        }
        Destroy(grid[7, 0].bw);
    }

    bool unvisited_neighbour(){
        if (unvisited_cell(cur_row-1, cur_col)){
            return true;
        }
        if (unvisited_cell(cur_row+1, cur_col)){
            return true;
        }
        if (unvisited_cell(cur_row, cur_col-1)){
            return true;
        }
        if (unvisited_cell(cur_row, cur_col+1)){
            return true;
        }
        return false;
    }

    bool visited_neighbour(int i, int j){
        if (visited_cell(i-1, j)){
            return true;
        }
        if (visited_cell(i+1, j)){
            return true;
        }
        if (visited_cell(i, j-1)){
            return true;
        }
        if (visited_cell(i, j+1)){
            return true;
        }
        return false;
    }

    bool unvisited_cell(int row, int col){
        if (row >= 0 && row <= 7 && col >= 0 && col <= 7){
            if (!grid[row, col].visited){
                return true;
            }
        }
        return false;
    }

    bool visited_cell(int row, int col){
        if (row >= 0 && row <= 7 && col >= 0 && col <= 7){
            if (grid[row, col].visited){
                return true;
            }
        }
        return false;
    }

    void curser(){
        while (unvisited_neighbour()){
            int direction = Random.Range(0,4);
            if (direction == 0){
                if (unvisited_cell(cur_row-1, cur_col)){
                    if (grid[cur_row, cur_col].fw){
                        Destroy(grid[cur_row, cur_col].fw);
                    }
                    cur_row--;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].bw){
                        Destroy(grid[cur_row, cur_col].bw);
                    }
                }
            }
            else if (direction == 1){
                if (unvisited_cell(cur_row+1, cur_col)){
                    if (grid[cur_row, cur_col].bw){
                        Destroy(grid[cur_row, cur_col].bw);
                    }
                    cur_row++;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].fw){
                        Destroy(grid[cur_row, cur_col].fw);
                    }
                }
            }
            else if (direction == 2){
                if (unvisited_cell(cur_row, cur_col-1)){
                    if (grid[cur_row, cur_col].rw){
                        Destroy(grid[cur_row, cur_col].rw);
                    }
                    cur_col--;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].lw){
                        Destroy(grid[cur_row, cur_col].lw);
                    }
                }
            }
            else {
                if (unvisited_cell(cur_row, cur_col+1)){
                    if (grid[cur_row, cur_col].lw){
                        Destroy(grid[cur_row, cur_col].lw);
                    }
                    cur_col++;
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col].rw){
                        Destroy(grid[cur_row, cur_col].rw);
                    }
                }
            }
        }
    }

    void restart(){
        finished = true;
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < rows; j++){
                if (!grid[i,j].visited && visited_neighbour(i,j)){
                    finished = false;
                    cur_row = i;
                    cur_col = j;
                    grid[cur_row, cur_col].visited = true; 
                    destroy_neighbour();
                    return;
                }
            }
        }
    }

    void destroy_neighbour(){
        bool destroyed = false;
        while (!destroyed){
            int direction = Random.Range(0,4);
            if (direction == 0){
                if (visited_cell(cur_row-1, cur_col)){
                    if (grid[cur_row, cur_col].fw){
                        Destroy(grid[cur_row, cur_col].fw);
                    }
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row-1, cur_col].bw){
                        Destroy(grid[cur_row-1, cur_col].bw);
                    }
                    destroyed = true;
                }
            }
            else if (direction == 1){
                if (visited_cell(cur_row+1, cur_col)){
                    if (grid[cur_row, cur_col].bw){
                        Destroy(grid[cur_row, cur_col].bw);
                    }
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row+1, cur_col].fw){
                        Destroy(grid[cur_row+1, cur_col].fw);
                    }
                    destroyed = true;
                }
            }
            else if (direction == 2){
                if (visited_cell(cur_row, cur_col-1)){
                    if (grid[cur_row, cur_col].rw){
                        Destroy(grid[cur_row, cur_col].rw);
                    }
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col-1].lw){
                        Destroy(grid[cur_row, cur_col-1].lw);
                    }
                    destroyed = true;
                }
            }
            else {
                if (visited_cell(cur_row, cur_col+1)){
                    if (grid[cur_row, cur_col].lw){
                        Destroy(grid[cur_row, cur_col].lw);
                    }
                    grid[cur_row, cur_col].visited = true;
                    if (grid[cur_row, cur_col+1].rw){
                        Destroy(grid[cur_row, cur_col+1].rw);
                    }
                    destroyed = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider otherObj) {
        if (otherObj.gameObject.tag == "Floor"){
            if (!otherObj.gameObject.GetComponent<tile>().on){
                time += 1;
                show_way();
                build_solution();
                cur_row = 0;
                cur_col = 0;
                aldous_broder();
                otherObj.gameObject.GetComponent<tile>().on = true;
            }
        }
        if (otherObj.gameObject.tag == "Win"){
            Instantiate(message, new Vector3(9, 0.2f, 29), Quaternion.identity);
            time = 0;
            gameObject.transform.position = new Vector3(10, 2.5f, 2);
            gameObject.GetComponent<CharacterController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || time > 16){
            time = 0;
            gameObject.transform.position = new Vector3(10, 2.5f, 2);
            show_way();
            build_solution();
            cur_row = 0;
            cur_col = 0;
            aldous_broder();
        }
    }

    void OnGUI() {
        GUI.Button(new Rect (10,30,150,20), "Time spent: " + time.ToString());
    }
}
