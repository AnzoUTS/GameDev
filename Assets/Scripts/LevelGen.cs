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
        Quaternion Qua = Quaternion.Euler(0, 0, 270);

        float Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        float ColLength = levelMap.GetLength(1); // number of columns in array : 14
        float ArrayPos = ColLength;
        float Adj_X;

        Pos_X = Center_X;
        Pos_Y = ColLength + 1;


        int levelMapSize = levelMap.Length;
        Debug.Log("LevelSIZE : " + levelMapSize);  // get rotation of previous?

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
                    Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], new Vector3(Adj_X, Pos_Y, 0), Qua);
                   // Debug.Log("Vec-POS: " + new Vector3(Adj_X, Pos_Y, 0));
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

                if (ArrayPos <= 0)
                {
                    ArrayPos = ColLength;
                }
            }

            Pos_Y--;

        }
    }



    void Quad2()
    {

    Quaternion Qua = Quaternion.Euler(0, 0, 0);

    int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
    int ColLength = levelMap.GetLength(1); // number of columns in array : 14

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
                    Instantiate(TileMaps[1], Vec3, Qua);
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], Vec3, Qua);
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
    }

    void Quad3()
    {

        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14
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
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 1)
                {
                    Instantiate(TileMaps[1], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Pos_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Pos_X, Adj_Y, 0));
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
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 1)
                {
                    Instantiate(TileMaps[1], new Vector3(Adj_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 2)
                {
                    Instantiate(TileMaps[2], new Vector3(Adj_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 3)
                {
                    Instantiate(TileMaps[3], new Vector3(Adj_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 4)
                {
                    Instantiate(TileMaps[4], new Vector3(Adj_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 5)
                {
                    Instantiate(TileMaps[5], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 6)
                {
                    Instantiate(TileMaps[6], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
                }
                else if (levelMap[row, col] == 7)
                {
                    Instantiate(TileMaps[7], new Vector3(Adj_X, Adj_Y, 0), Qua);
                    Debug.Log("Vec-POS: " + new Vector3(Adj_X, Adj_Y, 0));
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


