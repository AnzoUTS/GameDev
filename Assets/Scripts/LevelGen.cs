using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject DestroyWalls;
    public GameObject DestroyItems;
    public GameObject DestroyEnemies;
    public GameObject[] TileMaps;
    
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
     CreateLevel();
    }

    void DestroyGameObject()
    {
        Destroy(DestroyWalls);
        Destroy(DestroyItems);
        Destroy(DestroyEnemies);
    }

    void CreateLevel()
    {

        int row = 0;
        int col = 0;
        int Pos_X = 0;
        int Pos_Y = 14;

        int Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        int ColLength = levelMap.GetLength(1); // number of columns in array : 14

        for (row = 0; row < Rowlength; row++)
        {

            for (col = 0; col < ColLength; col++)
            {

                Quaternion Qua = Quaternion.Euler(0, 0, 90);
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


                if (Pos_X == 14)
                {
                    Pos_X = 0;
                }


            }


            Pos_Y--;

        }
    }



}


