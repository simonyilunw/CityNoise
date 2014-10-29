using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CityNoise
{
    public partial class getTensor : System.Web.UI.Page
    {
        double[, ,] weekdayTensor = new double[1200, 15, 25];
        double[, ,] weekendTensor = new double[1200, 15, 25];
        int[] rm = new int[1954];
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            readTensor();
            string method = Request.QueryString["method"];
            int tID = Convert.ToInt32( Request.QueryString["tID"]);
            int cID = Convert.ToInt32( Request.QueryString["cID"]);
            int wID = Convert.ToInt32(Request.QueryString["wID"]);
            double[] tensorValue = new double[1200];
            if (method == "allRegion")
            {
                if (tID == 0 && cID == 0)
                {
                    if (wID == 0)
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int c = 1; c <= 14; c++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    tensorValue[r] += weekdayTensor[r, c, t];
                                }
                    }
                    else
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int c = 1; c <= 14; c++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    tensorValue[r] += weekendTensor[r, c, t];
                                }

                    }
                }
                else if (tID == 0)
                {
                    if (wID == 0)
                    {
                        for (int t = 1; t <= 24; t++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    tensorValue[r] += weekdayTensor[r, cID, t];
                                }
                    }
                    else
                    {
                        for (int t = 1; t <= 24; t++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    tensorValue[r] += weekendTensor[r, cID, t];
                                }

                    }
                
                }
                else if (cID == 0)
                {
                    if (wID == 0)
                    {
                        for (int c = 1; c <= 14; c++)
                            for (int r = 1; r <= 1199; r++)
                            {
                                tensorValue[r] += weekdayTensor[r, c, tID];
                            }
                    }
                    else
                    {
                        for (int c = 1; c <= 14; c++)
                            for (int r = 1; r <= 1199; r++)
                            {
                                tensorValue[r] += weekendTensor[r, c, tID];
                            }

                    }
                }
                else
                {
                    if (wID == 0)
                    {
                            for (int r = 1; r <= 1199; r++)
                            {
                                tensorValue[r] += weekdayTensor[r, cID, tID];
                            }
                    }
                    else
                    {
                            for (int r = 1; r <= 1199; r++)
                            {
                                tensorValue[r] += weekendTensor[r, cID, tID];
                            }

                    }
                }
                double[] returnValue = new double[1954];
                string returnString = "0";
                for (int r = 1; r <= 1953; r++)
                {
                    if (rm[r] == 0)
                    {
                        returnString += " 0";
                    }
                    else
                    {
                        returnString += " " + tensorValue[rm[r]];
                    }
                }
                Response.Write(returnString);

            }
            else if (method == "allTime")
            {
                string validRegion = "892 898 912 921 922 925 927 930 932 933 935 936 937 938 939 940 941 942 943 945 946 947 948 949 951 952 954 955 956 957 958 962 963 964 965 966 967 969 970 972 973 974 975 976 977 980 981 983 984 985 986 990 991 994 995 998 999 1004 1010 1011 1012 1015 1016 1020 1021 1029 1031 1040 1044 1045 1046 1047 1050 1051 1052 1053 1057 1059 1060 1061 1064 1065 1069 1072 1077 1078 1079 1081 1082 1084 1085 1086 1087 1091 1099 1100 1101 1102 1105 1107 1108 1111 1113 1114 1115 1116 1119 1120 1123 1124 1127 1128 1131 1132 1133 1136 1137 1140 1143 1145 1146 1147 1148 1150 1151 1154 1157 1160 1162 1164 1169 1170 1176 1179 1180 1185 1190 1191 1192 1194 1195 1196 1199 1200 1203 1204 1205 1207 1210 1213 1214 1216 1217 1219 1220 1221 1222 1225 1228 1231 1232 1234 1235 1237 1242 1244 1245 1246 1247 1248 1249 1251 1253 1255 1256 1263 1265 1268 1270 1272 1275 1276 1277 1278 1280 1281 1282 1284 1285 1286 1288 1292 1297 1299 1300 1302 1303 1304 1307 1309 1310 1311 1312 1319 1320 1321 1322 1323 1329 1332 1333 1335 1336 1337 1339 1340 1341 1348 1350 1351 1353 1354 1355 1356 1358 1359 1360 1366 1369 1370 1371 1374 1377 1378 1379 1380 1381 1382 1383 1384 1385 1386 1387 1388 1389 1390 1392 1394 1396 1397 1398 1399 1401 1402 1403 1404 1405 1406 1407 1408 1409 1410 1411 1412 1413 1415 1416 1420 1421 1422 1423 1425 1428 1429 1430 1431 1432 1436 1437 1438 1439 1440 1442 1443 1445 1446 1448 1457 1458 1461 1462 1465 1467 1470 1473 1474 1475 1476 1478 1479 1482 1483 1484 1485 1488 1492 1493 1494 1495 1496 1497 1501 1502 1504 1506 1507 1508 1509 1510 1511 1513 1514 1515 1516 1517 1518 1520 1521 1522 1523 1524 1525 1526 1527 1530 1532 1533 1535 1540";
                int[] validRegionList = new int[1200];
                string[] tmp = validRegion.Split(' ');
                for (int r = 0; r < tmp.Length; r++)
                {
                    validRegionList[rm[Convert.ToInt32(tmp[r])]] = 1;
                }

                double[] returnValue = new double[25];
                if (cID == 0)
                {
                    if (wID == 0)
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int c = 1; c <= 14; c++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    if (validRegionList[r] == 1)
                                    {
                                        returnValue[t] += weekdayTensor[r, c, t];
                                    }
                                }
                    }
                    else
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int c = 1; c <= 14; c++)
                                for (int r = 1; r <= 1199; r++)
                                {
                                    if (validRegionList[r] == 1)
                                    {
                                        returnValue[t] += weekendTensor[r, c, t];
                                    }
                                }

                    }
                }
                else
                {
                    if (wID == 0)
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int r = 1; r <= 1199; r++)
                            {
                                if (validRegionList[r] == 1)
                                {
                                    returnValue[t] += weekdayTensor[r, cID, t];
                                }
                            }
                    }
                    else
                    {
                        for (int t = 1; t <= 24; t++)
                            for (int r = 1; r <= 1199; r++)
                            {
                                if (validRegionList[r] == 1)
                                {
                                    returnValue[t] += weekendTensor[r, cID, t];
                                }
                            }

                    }

                }
                string returnString = "0";
                for (int t = 1; t <= 24; t++)
                {
                    
                    returnString += " " + returnValue[t];
                    
                }
                Response.Write(returnString);
            }
            else if (method == "allCategory")
            {
                int rID = rm[ Convert.ToInt32(Request.QueryString["rID"])];
                double[,] matrixValue = new double[15, 25];
                Tuple<int, double>[] sumValue = new Tuple<int, double>[15];
                for (int c = 1; c <= 14; c++)
                {
                    sumValue[c] = new Tuple<int, double>(c, 0);
                }

                if (wID == 0)
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                        {
                            matrixValue[c, t] = weekdayTensor[rID, c, t];
                            sumValue[c] = new Tuple<int, double>(sumValue[c].Item1, sumValue[c].Item2 + weekdayTensor[rID, c, t]);
                        }
                }
                else
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                        {
                            matrixValue[c, t] = weekendTensor[rID, c, t];
                            sumValue[c] = new Tuple<int, double>(sumValue[c].Item1, sumValue[c].Item2 + weekendTensor[rID, c, t]);
                        }
                }
                Tuple<int, double> temp;
                for (int i = 1; i <= 13; i++)
                    for (int j = i + 1; j <= 14; j++)
                    {
                        if (sumValue[i].Item2 < sumValue[j].Item2)
                        {
                            temp = sumValue[i];
                            sumValue[i] = sumValue[j];
                            sumValue[j] = temp;
                        }
                    }


                double[] returnValue = new double[25];
                for (int c = 1; c <= 5; c++)
                {
                    for (int t = 1; t <= 24; t++)
                    {

                         returnValue[t] += matrixValue[sumValue[c].Item1, t];


                    }
                }
                double maxReturnValue = 0;
                for (int t = 1; t <= 24; t++)
                {
                    if (returnValue[t] > maxReturnValue)
                    {
                        maxReturnValue = returnValue[t];   
                    }
                }

                string returnString = "0";
                for (int c = 1; c <= 5; c++)
                {
                    returnString += " " + sumValue[c].Item1;
                    for (int t = 1; t <= 24; t++)
                    {

                        returnString += " " + matrixValue[sumValue[c].Item1, t] / maxReturnValue;


                    }
                }
                Response.Write(returnString);
            }
            else if (method == "overallTime")
            {
                string validRegion = "892 898 912 921 922 925 927 930 932 933 935 936 937 938 939 940 941 942 943 945 946 947 948 949 951 952 954 955 956 957 958 962 963 964 965 966 967 969 970 972 973 974 975 976 977 980 981 983 984 985 986 990 991 994 995 998 999 1004 1010 1011 1012 1015 1016 1020 1021 1029 1031 1040 1044 1045 1046 1047 1050 1051 1052 1053 1057 1059 1060 1061 1064 1065 1069 1072 1077 1078 1079 1081 1082 1084 1085 1086 1087 1091 1099 1100 1101 1102 1105 1107 1108 1111 1113 1114 1115 1116 1119 1120 1123 1124 1127 1128 1131 1132 1133 1136 1137 1140 1143 1145 1146 1147 1148 1150 1151 1154 1157 1160 1162 1164 1169 1170 1176 1179 1180 1185 1190 1191 1192 1194 1195 1196 1199 1200 1203 1204 1205 1207 1210 1213 1214 1216 1217 1219 1220 1221 1222 1225 1228 1231 1232 1234 1235 1237 1242 1244 1245 1246 1247 1248 1249 1251 1253 1255 1256 1263 1265 1268 1270 1272 1275 1276 1277 1278 1280 1281 1282 1284 1285 1286 1288 1292 1297 1299 1300 1302 1303 1304 1307 1309 1310 1311 1312 1319 1320 1321 1322 1323 1329 1332 1333 1335 1336 1337 1339 1340 1341 1348 1350 1351 1353 1354 1355 1356 1358 1359 1360 1366 1369 1370 1371 1374 1377 1378 1379 1380 1381 1382 1383 1384 1385 1386 1387 1388 1389 1390 1392 1394 1396 1397 1398 1399 1401 1402 1403 1404 1405 1406 1407 1408 1409 1410 1411 1412 1413 1415 1416 1420 1421 1422 1423 1425 1428 1429 1430 1431 1432 1436 1437 1438 1439 1440 1442 1443 1445 1446 1448 1457 1458 1461 1462 1465 1467 1470 1473 1474 1475 1476 1478 1479 1482 1483 1484 1485 1488 1492 1493 1494 1495 1496 1497 1501 1502 1504 1506 1507 1508 1509 1510 1511 1513 1514 1515 1516 1517 1518 1520 1521 1522 1523 1524 1525 1526 1527 1530 1532 1533 1535 1540";
                int[] validRegionList = new int[1200];
                string[] tmp = validRegion.Split(' ');
                for (int r = 0; r < tmp.Length; r++)
                {
                    validRegionList[rm[Convert.ToInt32(tmp[r])]] = 1;
                }

                double[] returnValue = new double[15];

                if (wID == 0)
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                            for (int r = 1; r <= 1199; r++ )
                            {
                                if (validRegionList[r] == 1)
                                {
                                    returnValue[c] += weekdayTensor[r, c, t];
                                }
                            }
                }
                else
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                            for (int r = 1; r <= 1199; r++)
                            {
                                if (validRegionList[r] == 1)
                                {
                                    returnValue[c] += weekendTensor[r, c, t];
                                }
                            }
                }

                Tuple<int, double>[] sumValue = new Tuple<int, double>[15];
                for (int c = 1; c <= 14; c++)
                {
                    sumValue[c] = new Tuple<int, double>(c, returnValue[c]);
                }

                Tuple<int, double> temp;
                for (int i = 1; i <= 13; i++)
                    for (int j = i + 1; j <= 14; j++)
                    {
                        if (sumValue[i].Item2 < sumValue[j].Item2)
                        {
                            temp = sumValue[i];
                            sumValue[i] = sumValue[j];
                            sumValue[j] = temp;
                        }
                    }

                double sumSumValue = 0;
                for (int c = 1; c <= 14; c++)
                {
                     sumSumValue += sumValue[c].Item2;
                    
                }
                string returnString = "0";
                for (int c = 1; c <= 8; c++)
                {
                    returnString += " " + sumValue[c].Item1 + " " + sumValue[c].Item2 / sumSumValue;
                }
                Response.Write(returnString);
            }
            else if (method == "overallTimeInRegion")
            {
                int rID = rm[Convert.ToInt32(Request.QueryString["rID"])];
                double[,] matrixValue = new double[15, 25];
                Tuple<int, double>[] sumValue = new Tuple<int, double>[15];
                for (int c = 1; c <= 14; c++)
                {
                    sumValue[c] = new Tuple<int, double>(c, 0);
                }

                if (wID == 0)
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                        {
                            matrixValue[c, t] = weekdayTensor[rID, c, t];
                            sumValue[c] = new Tuple<int, double>(sumValue[c].Item1, sumValue[c].Item2 + weekdayTensor[rID, c, t]);
                        }
                }
                else
                {
                    for (int c = 1; c <= 14; c++)
                        for (int t = 1; t <= 24; t++)
                        {
                            matrixValue[c, t] = weekendTensor[rID, c, t];
                            sumValue[c] = new Tuple<int, double>(sumValue[c].Item1, sumValue[c].Item2 + weekendTensor[rID, c, t]);
                        }
                }
                Tuple<int, double> temp;
                for (int i = 1; i <= 13; i++)
                    for (int j = i + 1; j <= 14; j++)
                    {
                        if (sumValue[i].Item2 < sumValue[j].Item2)
                        {
                            temp = sumValue[i];
                            sumValue[i] = sumValue[j];
                            sumValue[j] = temp;
                        }
                    }

                double sumSumValue = 0;
                for (int c = 1; c <= 14; c++)
                {
                    sumSumValue += sumValue[c].Item2;

                }
                string returnString = "0";
                for (int c = 1; c <= 8; c++)
                {
                    returnString += " " + sumValue[c].Item1 + " " + sumValue[c].Item2 / sumSumValue;
                }
                Response.Write(returnString);
            }

        }

        void readTensor()
        {

            StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/weekday.txt");
            for (int i = 1; i <= 1199; i++)
                for (int j = 1; j <= 14; j++ )
                    for (int k = 1; k <= 24; k++)
                    {
                        weekdayTensor[i, j, k] = Convert.ToDouble(sr.ReadLine());                   
                    }
            sr.Close();
            sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/weekend.txt");
            for (int i = 1; i <= 1199; i++)
                for (int j = 1; j <= 14; j++)
                    for (int k = 1; k <= 24; k++)
                    {
                        weekendTensor[i, j, k] = Convert.ToDouble(sr.ReadLine());
                    }
            sr.Close();
            sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/RM.txt");
            for (int i = 1; i <= 1953; i++)
                    {
                        rm[i] = Convert.ToInt32(sr.ReadLine().Trim());
                    }
            sr.Close();
        }
    }
}