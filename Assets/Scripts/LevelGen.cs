using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject DestroyWalls;
    public GameObject DestroyItems;
    public GameObject DestroyEnemies;
    public GameObject[] TileMaps;
    private float Pos_X;
    private float Pos_Y;
    //private float ArrayPos;

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

    void Start()
    {
       DestroyGameObject();
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
        float ArrayPos = ColLength;
        float Adj_X;

        Pos_X = 2;
        Pos_Y = ColLength + 1;

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
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 1)
                {
                    Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Adj_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }

                ArrayPos--;

                Debug.Log("ArrayPos: " + ArrayPos);
                if (ArrayPos <= 0)
                {
                    ArrayPos = ColLength;
                }

                /*     Pos_X++;*/
                /*               if (Pos_X == ColLength + Pos_X)
                               {
                                   Pos_X = 2f;
                               }*/
            }

            Pos_Y--;

/*            if (Pos_Y == 0f)
            {
                Pos_Y = ColLength;
            }*/


        }
    }



    void Quad2()
    {

 
        Quaternion Qua = Quaternion.Euler(0, 0, 90);

    int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
    int ColLength = levelMap.GetLength(1); // number of columns in array : 14


/*        Pos_X = -12f;
        Pos_Y = 15f;*/

        Pos_X = -Rowlength+3;
        Pos_Y = ColLength+1;

        Debug.Log(Rowlength);
        Debug.Log(ColLength);

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
                    Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-1: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-2: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], Vec3, Qua);
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], Vec3, Qua);
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], Vec3, Qua);
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], Vec3, Qua);
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], Vec3, Qua);
                }

                Pos_X++;

                Debug.Log("X: " + Pos_X + " Y :" + Pos_Y);

                if (Pos_X == 2f)
                {
                    Pos_X = -12f;
                }
            }

            Pos_Y--;

            if (Pos_Y == -14f)
            {
                Pos_Y = 14f;
            }


        }
    }

    void Quad3()
    {

        Pos_X = -12f;
        Pos_Y = 0;

        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14

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
                    Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-1: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-2: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], Vec3, Qua);
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], Vec3, Qua);
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], Vec3, Qua);
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], Vec3, Qua);
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], Vec3, Qua);
                }

                Pos_X++;

                Debug.Log("X: " + Pos_X + " Y :" + Pos_Y);

                if (Pos_X == 2f)
                {
                    Pos_X = -12f;
                }
            }

            Pos_Y--;

            if (Pos_Y == -15f)
            {
                Pos_Y = 0f;
            }


        }
    }


    void Quad4()
    {
        
        Pos_X = 2f;
        Pos_Y = 0f;

        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14

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
                    Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-1: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Qua);
                    Debug.Log("Vec-2: " + new Vector3(Pos_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], Vec3, Qua);
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], Vec3, Qua);
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], Vec3, Qua);
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], Vec3, Qua);
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], Vec3, Qua);
                }

                Pos_X++;

                Debug.Log("X: " + Pos_X + " Y :" + Pos_Y);

                if (Pos_X == 16f)
                {
                    Pos_X = 2f;
                }
            }

            Pos_Y--;

            if (Pos_Y == -15f)
            {
                Pos_Y = 0f;
            }


        }
    }





}


