using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject DestroyWalls;
    public GameObject DestroyItems;
    public GameObject DestroyEnemies;
    public GameObject[] TileMaps;
    private float Center_X;
    private float Center_Y;
    private float Pos_X;
    private float Pos_Y;


    public int[,] levelMap = {
{1,2,2,2,2,2,2,2,2,2,2,2,2,7},
{2,5,5,5,5,5,5,5,5,5,5,5,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,4},
{2,6,4,0,0,4,5,4,0,0,0,4,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,3},
{2,5,5,5,5,5,5,5,5,5,5,5,5,5},
{2,5,3,4,4,3,5,3,3,5,3,4,4,4},
{2,5,3,4,4,3,5,4,4,5,3,4,4,3},
{2,5,5,5,5,5,5,4,4,5,5,5,5,4},
{1,2,2,2,2,1,5,4,3,4,4,3,0,4},
{0,0,0,0,0,2,5,4,3,4,4,3,0,3},
{0,0,0,0,0,2,5,4,4,0,0,0,0,0},
{0,0,0,0,0,2,5,4,4,0,3,4,4,0},
{2,2,2,2,2,1,5,3,3,0,4,0,0,0},
{0,0,0,0,0,0,5,0,0,0,4,0,0,0},
};


    /*    public int[,] levelMap = { // test array large
    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,7},
    {2,2,2,5,5,5,5,5,5,5,5,5,5,5,5,4},
    {2,2,2,5,3,4,4,3,5,3,4,4,4,3,5,4},
    {2,2,2,6,4,0,0,4,5,4,0,0,0,4,5,4},
    {2,2,2,5,3,4,4,3,5,3,4,4,4,3,5,3},
    {2,2,2,5,5,5,5,5,5,5,5,5,5,5,5,5},
    {2,2,2,5,3,4,4,3,5,3,3,5,3,4,4,4},
    {2,2,2,5,3,4,4,3,5,4,4,5,3,4,4,3},
    {2,2,2,5,5,5,5,5,5,4,4,5,5,5,5,4},
    {1,2,2,2,2,2,2,1,5,4,3,4,4,3,0,4},
    {0,0,0,0,0,0,0,2,5,4,3,4,4,3,0,3},
    {0,0,0,0,0,0,0,2,5,4,4,0,0,0,0,0},
    {0,0,0,0,0,0,0,2,5,4,4,0,3,4,4,0},
    {2,2,2,2,2,2,2,1,5,3,3,0,4,0,0,0},
    {0,0,0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    {0,0,0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    {0,0,0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    */


    /*    public int[,] levelMap = { // test 2 small
    {1,2,2,2,2,2,2,2,2,2,2,2},
    {2,5,5,5,5,5,5,5,5,5,5,5},
    {2,5,3,4,4,3,5,3,4,4,4,3},
    {2,6,4,0,0,4,5,4,0,0,0,4},
    {2,5,3,4,4,3,5,3,4,4,4,3},
    {2,5,5,5,5,5,5,5,5,5,5,5},
    {2,5,3,4,4,3,5,3,3,5,3,4},
    {2,5,3,4,4,3,5,4,4,5,3,4},
    {2,5,5,5,5,5,5,4,4,5,5,5},
    {1,2,2,2,2,1,5,4,3,4,4,3},
    {0,0,0,0,0,2,5,4,3,4,4,3},
    {0,0,0,0,0,2,5,4,4,0,0,0},
    };*/



    void Start()
    {
        DestroyGameObject();
        Center_X = 2;
        Center_Y = 0;
        Quad1();
        Quad2();
        Quad3();
        Quad4();
    }

    void DestroyGameObject()
    {
        Destroy(DestroyWalls);
        Destroy(DestroyItems);
        Destroy(DestroyEnemies);
    }


    void Quad1()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        float Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        float ColLength = levelMap.GetLength(1); // number of columns in array : 14
        List<GameObject> Q1_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)

        float ArrayPos = ColLength;
        float Adj_X;

        Pos_X = Center_X;
        Pos_Y = ColLength + 1;
        float angle = 0;



        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                //Vector3 Vec3 = new Vector3(Pos_X + ArrayPos - ArrayAdjust, Pos_Y, 0);
                //Adjust = Pos_X + ArrayPos - Pos_X + 1;
                Adj_X = ArrayPos + 1;

                if (levelMap[row, col] == 0)
                {


                    Instantiate(TileMaps[0], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));

                }
                else if (levelMap[row, col] == 1)
                {

                    if (Q1_TilesOutCorner.Count == 0)
                    {
                        Q1_TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 270)));
                        Debug.Log("FIRST Q1");
                    }
                    else
                    {

                        Q1_TilesOutCorner.Add (Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                    }
                }
                else if (levelMap[row, col] == 2)
                {

                    

                    if (Q1_TilesOutside.Count == 0)
                    {
                        Q1_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 90)));
                    }

                    else if (Q1_TilesOutside[Q1_TilesOutside.Count - 1].transform.position.y == Pos_Y || Q1_TilesOutside[Q1_TilesOutside.Count - 1].transform.position.x == Adj_X)
                    {
                        Debug.Log("############### WALL2 ELSE IF :" + Q1_TilesOutside[Q1_TilesOutside.Count - 1].transform.position);
                        Q1_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, Q1_TilesOutside[Q1_TilesOutside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q1_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, angle)));

                        if (angle == 0)
                        {
                            angle = 90;
                        }
                        else
                        {
                            angle = 0;

                        }
                    }




                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    if (Q1_TilesInside.Count == 0)
                    {
                        Q1_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0)));
                        //Debug.Log("Triggered 0 :" + Q1_TilesInside[Q1_TilesInside.Count - 1].transform.position);
                    }

                    else if (Q1_TilesInside[Q1_TilesInside.Count - 1].transform.position.y == Pos_Y && (Q1_TilesInside[Q1_TilesInside.Count - 1].transform.position.x - Adj_X) >1)
                    // && TilesOutCorner[TilesOutCorner.Count - 1].transform.position.y == Pos_Y
                    {
                        Q1_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 90)));
                        //Debug.Log("Triggered ELSE IF :" + Q1_TilesInside[Q1_TilesInside.Count - 1].transform.position);
                    }
                    else
                    {
                        Q1_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, Q1_TilesInside[Q1_TilesInside.Count - 1].transform.eulerAngles.z)));
                        //Debug.Log("Triggered ELSE :" + Q1_TilesInside[Q1_TilesInside.Count - 1].transform.position);
                    }
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                  //  Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }

                ArrayPos--;
               // Debug.Log("ArrayPOS :" + ArrayPos);

                if (ArrayPos <= 0)
                {
                    ArrayPos = ColLength;
                }
            }

            Pos_Y--;

        }


/*        IList list = GameTilesQ1;
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("LIST_Q1 :" + i + " : " + GameTilesQ1[i].transform + " : " + GameTilesQ1[i].transform.rotation.eulerAngles + " : " + GameTilesQ1[i].transform.position);
        }
*/


    }



    void Quad2()
    {

    Quaternion Qua = Quaternion.Euler(0, 0, 0);

    int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
    int ColLength = levelMap.GetLength(1); // number of columns in array : 14
    List<GameObject> Q2_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    float angle = 0;
    float angle2 = 0;


        Pos_X = Center_X - Rowlength +1;
        Pos_Y = ColLength + 1;

        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {

                Vector3 Vec3 = new Vector3(Pos_X, Pos_Y, 0);

                if (levelMap[row, col] == 0)
                {
                    Instantiate(TileMaps[0], Vec3, Qua);
                }
                else if (levelMap[row, col] == 1)
                {

                    if (Q2_TilesOutCorner.Count == 0)
                    {
                        Q2_TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0)));
                        Debug.Log("FIRST Q2");
                    }
                    else
                    {

                        Instantiate(TileMaps[1], Vec3, Qua);
                    }
                }
                else if (levelMap[row, col] == 2)
                {

                    if (Q2_TilesOutside.Count == 0)
                    {
                        Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 90)));
                    }

                    else if (Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position.y == Pos_Y || Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position.x == Pos_X)
                    {

                        Debug.Log("# Q2 #### WALL2 ELSE IF :" + Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position + " ||  PX " + Pos_X + " ||  PY " + Pos_Y);
                        Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, angle)));
                        Debug.Log("# Q2 #### ANGLE CHANGE :" + Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position + " ||  PX " + Pos_X + " ||  PY " + Pos_Y);

                        if (angle == 0)
                        {
                            angle = 90;
                        }
                        else
                        {
                            angle = 0;

                        }
                    }

                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], Vec3, Qua);
                }




                else if (levelMap[row, col] == 4)
                {
                    if (Q2_TilesInside.Count == 0)
                    {
                        Q2_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0)));
                    }

                    else if (Q2_TilesInside[Q2_TilesInside.Count - 1].transform.position.y == Pos_Y || Q2_TilesInside[Q2_TilesInside.Count - 1].transform.position.x == Pos_X)
                    {
                        Debug.Log("####### WALL4 ELSE IF ######## :" + Q2_TilesInside[Q2_TilesInside.Count - 1].transform.position);
                        Q2_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, Q2_TilesInside[Q2_TilesInside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q2_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, angle)));

                        if (angle2 == 0)
                        {
                            angle2 = 90;
                        }
                        else
                        {
                            angle2 = 0;

                        }
                    }

                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], Vec3, Quaternion.Euler(0, 0, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], Vec3, Quaternion.Euler(0, 0, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], Vec3, Qua);
                }

                Pos_X++;

                if (Pos_X == Center_X)
                {
                    Pos_X = Center_X - Rowlength + 1;
                }


            }

            Pos_Y--;

        }

        IList list = Q2_TilesOutside;
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("LIST_Q2 :"+ i +" : " + Q2_TilesOutside[i].transform + " : " + Q2_TilesOutside[i].transform.rotation.eulerAngles + " : " + Q2_TilesOutside[i].transform.position);
        }

        int levelMapSize = levelMap.Length;
        Debug.Log("LevelSIZE : " + levelMapSize);  // get rotation of previous?

        int rowX = 1;

/*        for (int col = 0; col < ColLength; col++)
        {

            Debug.Log("T :" + levelMap[rowX, col]);

        }
        // }

        //}
*/

    }

    void Quad3()
    {

        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14
        List<GameObject> Q3_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        float angle = 0;


        float ArrayPos = ColLength;
        float Adj_Y;

        Pos_X = Center_X - Rowlength + 1;
        Pos_Y = -ColLength + 1;


        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {

                Adj_Y = -ArrayPos + 1;

                if (levelMap[row, col] == 0)
                {
                    Instantiate(TileMaps[0], new Vector3(Pos_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 1)
                {
                    Instantiate(TileMaps[1], new Vector3(Pos_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    if (Q3_TilesOutside.Count == 0)
                    {
                        Q3_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 90)));
                    }

                    else if (Q3_TilesOutside[Q3_TilesOutside.Count - 1].transform.position.y == Adj_Y || Q3_TilesOutside[Q3_TilesOutside.Count - 1].transform.position.x == Pos_X)
                    {
                        Debug.Log("######################################### WALL2 ELSE IF :" + Q3_TilesOutside[Q3_TilesOutside.Count - 1].transform.position);
                        Q3_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, Q3_TilesOutside[Q3_TilesOutside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q3_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, angle)));

                        if (angle == 0)
                        {
                            angle = 90;
                        }
                        else
                        {
                            angle = 0;

                        }
                    }
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Pos_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    //Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                   // Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                  //  Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Pos_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }

               

                Pos_X++;

                if (Pos_X == Center_X)
                {
                    Pos_X = Center_X - Rowlength + 1;
                }
            }

            ArrayPos--;

        }
    }


    void Quad4()
    {
        
        Quaternion Qua = Quaternion.Euler(0, 0, 180);

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14
        List<GameObject> Q4_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q4_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q4_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q4_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        float angle = 0;
        float angle2 = 0;


        float ArrayPosX = ColLength;
        float ArrayPosY = ColLength;
        float Adj_X;
        float Adj_Y;

        Pos_X = Center_X - Rowlength + 1;
        Pos_Y = -ColLength + 1;

        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {

                Adj_X = ArrayPosX + 1;
                Adj_Y = -ArrayPosY + 1;

                if (levelMap[row, col] == 0)
                {
                    Instantiate(TileMaps[0], new Vector3(Adj_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 1)
                {
                    Instantiate(TileMaps[1], new Vector3(Adj_X, Adj_Y, 0), Qua);
                  //  Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    if (Q4_TilesOutside.Count == 0)
                    {
                        Q4_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 90)));
                    }

                    else if (Q4_TilesOutside[Q4_TilesOutside.Count - 1].transform.position.y == Adj_Y || Q4_TilesOutside[Q4_TilesOutside.Count - 1].transform.position.x == Adj_X)
                    {
                        Debug.Log("######################################### WALL2 ELSE IF :" + Q4_TilesOutside[Q4_TilesOutside.Count - 1].transform.position);
                        Q4_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, Q4_TilesOutside[Q4_TilesOutside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q4_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, angle)));

                        if (angle == 0)
                        {
                            angle = 90;
                        }
                        else
                        {
                            angle = 0;

                        }
                    }
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Adj_X, Adj_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {

                    if (Q4_TilesInside.Count == 0)
                    {
                        Q4_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0)));
                    }

                    else if (Q4_TilesInside[Q4_TilesInside.Count - 1].transform.position.y == Adj_Y || Q4_TilesInside[Q4_TilesInside.Count - 1].transform.position.x == Adj_X)
                    {
                        Debug.Log("####### WALL4 ELSE IF ######## :" + Q4_TilesInside[Q4_TilesInside.Count - 1].transform.position);
                        Q4_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, Q4_TilesInside[Q4_TilesInside.Count - 1].transform.eulerAngles.z)));

                    }
                    else
                    {
                        Q4_TilesInside.Add(Instantiate(TileMaps[4], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, angle)));

                        if (angle2 == 0)
                        {
                            angle2 = 90;
                        }
                        else
                        {
                            angle2 = 0;

                        }
                    }









                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                  //  Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                  //  Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Adj_X, Adj_Y, 0), Qua);
                  //  Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }

                ArrayPosX--;

                if (ArrayPosX <= 0)
                {
                    ArrayPosX = ColLength;
                }
            }

            ArrayPosY--;


        }
    }





}


