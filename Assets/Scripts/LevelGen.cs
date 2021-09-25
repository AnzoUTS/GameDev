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
        };*/



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
        };
    */


    void Start()
    {
        DestroyGameObject();
        Center_X = 2;
        Center_Y = 0;

       // Instantiate(GameObject.Find("Q2"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
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


    List<GameObject> TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)








    int OutLoop(float x, float y, float Pos_X, float Pos_Y)
    {

        for (int i = 0; i < TilesOutCorner.Count; i++)
        {
            Vector3 wall_distance = TilesOutCorner[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);
          //  Vector3 wall_distance =  new Vector3(Pos_X, Pos_Y, 0)- TilesOutCorner[i].transform.position;
            Debug.Log(" ----------OUT C WALL1-------------- " + wall_distance);


            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                Debug.Log(" ----------OUT C1-------------- ");

                return 1;
            }
            else if (wall_distance.x == 0f && wall_distance.y == y)
            {

                Debug.Log(" ----------OUT C2 -------------- ");
                return 2;
            }
            else
            {

                continue;
            }
        }
        return 0;
    }




    int OutLoop2(float x, float y, float Pos_X, float Pos_Y)
    {

        for (int i = 0; i < TilesOutside.Count; i++)
        {
            Vector3 wall_distance = TilesOutside[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);
            //Vector3 wall_distance =  new Vector3(Pos_X, Pos_Y, 0)- TilesOutside[i].transform.position;
            Debug.Log(" ----------OUT WALL2-------------- " + wall_distance);

            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                Debug.Log(" ----------OUT W1-------------- ");

                return 3;
            }
            else if (wall_distance.x == 0f && wall_distance.y == y)
            {

                Debug.Log(" ----------OUT W2 -------------- ");
                return 4;
            }
            else
            {

                continue;
            }
        }
        return 0;
    }







    int BigLoop(float x, float y, float Pos_X, float Pos_Y)
    {

        for (int i = 0; i < TilesInCorner.Count; i++)
        {
            Vector3 wall_distance = TilesInCorner[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);


            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                Debug.Log(" ----------HITTTTTT SC1-------------- ");

                return 1;
            }
            else if (wall_distance.x == 0f && wall_distance.y == y)
            {

                Debug.Log(" ----------HITTTTTT SC2 -------------- ");
                return 2;
            }
            else
            {

                continue;
            }
        }
        return 0;
    }




    int BigLoop2(float x, float y, float Pos_X, float Pos_Y)
    {

        for (int i = 0; i < TilesInside.Count; i++)
        {
            Vector3 wall_distance = TilesInside[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);


            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                Debug.Log(" ----------HITTTTTT SSC1-------------- ");

                return 1;
            }
            else if (wall_distance.x == 0f && wall_distance.y == y)
            {

                Debug.Log(" ----------HITTTTTT SSC2 -------------- ");
                return 2;
            }
            else
            {

                continue;
            }
        }
        return 0;
    }



    void Quad1()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        float Rowlength = levelMap.GetLength(0); // number of rows in array : 15
        float ColLength = levelMap.GetLength(1); // number of columns in array : 14
/*        List<GameObject> Q1_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q1_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        float angle = 0;
        float angle2 = 90;*/
        float horizontal = 90f;
        float vertical = 0f;


        float ArrayPos = ColLength;
        float Adj_X;

        Pos_X = Center_X;
        Pos_Y = ColLength + 1;


        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                Adj_X = ArrayPos + 1;



                Vector3 pos = new Vector3(Adj_X, Pos_Y, 0);
                Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                switch (levelMap[row, col])
                {


                         


                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Adj_X, Pos_Y, 0), Qua);
                        break;

                    case 1:
                        if (TilesOutside.Count == 0)
                        {
                            TilesOutside.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 270)));
                        }
                        else
                        {
                            TilesOutside.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        }
                        break;
                    case 2:



                        if (TilesOutside.Count == 0) // never trigger?
                        {
                            TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                            break;

                        }
                        else if (OutLoop(-1, -1, Adj_X, Pos_Y) > 0)
                        {

                            if (OutLoop(-1, -1, Adj_X, Pos_Y) == 1)
                            {
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT C1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT C2 -------------- ");
                                break;

                            }

                        }

                        else if (OutLoop2(-1, -1, Adj_X, Pos_Y) > 2)
                        {

                            if (OutLoop2(-1, -1, Adj_X, Pos_Y) == 3)
                            {
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT W1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT W2 -------------- ");
                                break;

                            }

                        }



                        else
                        {

                            Debug.Log(" -----------NO IDEA------------- ");
                            TilesOutside.Add(Instantiate(TileMaps[2], pos, quat));
                            Debug.Log(" ############## " + pos + " ################## ");
                            break;



                        }






                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        break;
                    case 4:

                       // Vector3 pos = new Vector3(Adj_X, Pos_Y, 0);
                      //  Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                        if (TilesInside.Count == 0)
                        {
                            TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                            break;

                        }
                        else if (BigLoop(1, 1, Adj_X, Pos_Y) > 0)
                        {

                            if (BigLoop(1, 1, Adj_X, Pos_Y) == 1)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT C1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT C2 -------------- ");
                                break;

                            }

                        }

                        else if (BigLoop2(1, 1, Adj_X, Pos_Y) > 2)
                        {

                            if (BigLoop2(1, 1, Adj_X, Pos_Y) == 3)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" --------## --HITTTTTT W1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" -------## ---HITTTTTT W2 -------------- ");
                                break;

                            }

                        }
                        else
                        {

                            Debug.Log(" -----------NO IDEA------------- ");
                            TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                            Debug.Log(" ############## " + pos + " ################## ");
                            break;



                        }


                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        Instantiate(TileMaps[7], new Vector3(Adj_X, Pos_Y, 0), Qua);
                        break;

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
    List<GameObject> Q2_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> Q2_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    float angle = 0;
    float angle2 = 90;
        float horizontal = 90f;
        float vertical = 0f;


        Pos_X = Center_X - Rowlength +1;
        Pos_Y = ColLength + 1;






        for (int row = 0; row < Rowlength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {

                Vector3 Vec3 = new Vector3(Pos_X, Pos_Y, 0);




                switch (levelMap[row, col])
                {
                    case 0:
                        Instantiate(TileMaps[0], Vec3, Qua);
                        break;
                    case 1:
                        if (Q2_TilesOutCorner.Count == 0)
                        {
                            Q2_TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, angle)));
                            Debug.Log("###################################################################  FIRST Q2");
                            break;
                        }
                        else
                        {
                            Instantiate(TileMaps[1], Vec3, Qua);
                            break;
                        }
                    case 2:
                        if (Q2_TilesOutside.Count == 0)
                        {
                            Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 90)));
                            break;
                        }

                        else if (Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position.y == Pos_Y || Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.position.x == Pos_X)

                        {
                            Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, Q2_TilesOutside[Q2_TilesOutside.Count - 1].transform.eulerAngles.z)));
                            break;
                        }
                        else
                        {
                            Q2_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, angle)));
   

                            if (angle == 0)
                            {
                                angle = 90;
                            }
                            else
                            {
                                angle = 0;

                            }
                            break;
                        }

                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], Vec3, Qua));
                        break;


                    case 4:



                        Vector3 pos = new Vector3(Pos_X, Pos_Y, 0);
                        Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                        if (TilesInside.Count == 0)
                        {
                            TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                            break;

                        }
                        else if (BigLoop(1, 1, Pos_X, Pos_Y) > 0)
                        {

                            if (BigLoop(1, 1, Pos_X, Pos_Y) == 1)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT C1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT C2 -------------- ");
                                break;

                            }

                        }

                        else if (BigLoop2(1, 1,Pos_X, Pos_Y) > 0)
                        {

                            if (BigLoop2(1,1, Pos_X, Pos_Y) == 3)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" --------## --HITTTTTT W1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" -------## ---HITTTTTT W2 -------------- ");
                                break;

                            }

                        }
                        else
                        {

                            Debug.Log(" -----------NO IDEA------------- ");
                            TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                            Debug.Log(" ############## " + pos + " ################## ");
                            break;

                        }


                    case 5:

                        Instantiate(TileMaps[5], Vec3, Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], Vec3, Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        Q2_TilesInCorner.Add(Instantiate(TileMaps[7], Vec3, Qua));
                        break;

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
        List<GameObject> Q3_TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesOutside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesInCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        List<GameObject> Q3_TilesInside = new List<GameObject>(); // create a list (dont need to know the array size in advance)
        float angle = 0;
        float horizontal = 90f;
        float vertical = 0f;

        float ArrayPos = ColLength;
        float Adj_Y;

        Pos_X = Center_X - Rowlength + 1;
        Pos_Y = -ColLength + 1;


        for (int row = 0; row < Rowlength - 1; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {

                Adj_Y = -ArrayPos + 1;


                switch (levelMap[row, col])
                {
                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Pos_X, Adj_Y, 0), Qua);
                        break;
                    case 1:
                        Instantiate(TileMaps[1], new Vector3(Pos_X, Adj_Y, 0), Qua);
                        break;

                    case 2:
                        Q3_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 90)));
                        break;

                    case 3:


                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Pos_X,Adj_Y, 0), Qua));
                        break;


                    case 4:

                        Vector3 pos = new Vector3(Pos_X, Adj_Y, 0);
                        Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                        if (TilesInside.Count == 0)
                        {
                            TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                            break;

                        }
                        else if (BigLoop(-1, -1, Pos_X, Adj_Y) > 0)
                        {

                            if (BigLoop(-1, -1,Pos_X, Adj_Y) == 1)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT C1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT C2 -------------- ");
                                break;

                            }

                        }

                        else if (BigLoop2(-1, -1,Pos_X, Adj_Y) > 0)
                        {

                            if (BigLoop2(-1, -1,Pos_X, Adj_Y) == 3)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" --------## --HITTTTTT W1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" -------## ---HITTTTTT W2 -------------- ");
                                break;

                            }

                        }
                        else
                        {

                            Debug.Log(" -----------NO IDEA------------- ");
                            TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                            Debug.Log(" ############## " + pos + " ################## ");
                            break;

                        }


                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;

                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;

                    case 7:
                        Instantiate(TileMaps[7], new Vector3(Pos_X, Adj_Y, 0), Qua);
                        break;

                }

                Pos_X++;

                if (Pos_X == Center_X)
                {
                    Pos_X = Center_X - Rowlength + 1;
                }

           

                //break;

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
        float horizontal = 90f;
        float vertical = 0f;


        float ArrayPosX = ColLength;
        float ArrayPosY = ColLength;
        float Adj_X;
        float Adj_Y;

        Pos_X = Center_X - Rowlength + 1;
        Pos_Y = -ColLength + 1;

        for (int row = 0; row < Rowlength - 1; row++)
        {
            for (int col = 0; col < ColLength; col++)
            {

                Adj_X = ArrayPosX + 1;
                Adj_Y = -ArrayPosY + 1;


                switch (levelMap[row, col])
                {
                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Adj_X, Adj_Y, 0), Qua);
                        break;
                    case 1:
                        Instantiate(TileMaps[1], new Vector3(Adj_X, Adj_Y, 0), Qua);
                        break;

                    case 2:
                        Q4_TilesOutside.Add(Instantiate(TileMaps[2], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 90)));
                        break;

                    case 3:


                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Adj_X, Adj_Y, 0), Qua));
                        break;


                    case 4:

                        Vector3 pos = new Vector3(Adj_X, Adj_Y, 0);
                        Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                        if (TilesInside.Count == 0)
                        {
                            TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                            break;

                        }
                        else if (BigLoop(-1, -1, Adj_X, Adj_Y) > 0)
                        {

                            if (BigLoop(-1, -1, Adj_X, Adj_Y) == 1)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" ---------- ## HITTTTTT C1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" ---------## -HITTTTTT C2 -------------- ");
                                break;

                            }

                        }

                        else if (BigLoop2(-1, -1, Adj_X, Adj_Y) > 0)
                        {

                            if (BigLoop2(-1, -1, Adj_X, Adj_Y) == 3)
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                Debug.Log(" --------## --HITTTTTT W1-------------- ");
                                break;

                            }
                            else
                            {
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                Debug.Log(" -------## ---HITTTTTT W2 -------------- ");
                                break;

                            }

                        }
                        else
                        {

                            Debug.Log(" -----------NO IDEA------------- ");
                            TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                            Debug.Log(" ############## " + pos + " ################## ");
                            break;

                        }


                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;

                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;

                    case 7:
                        Instantiate(TileMaps[7], new Vector3(Adj_X, Adj_Y, 0), Qua);
                        break;

 
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


