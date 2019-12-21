#region << 信 息 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：RecommendLDSDemo
* 项目描述 ：基于物品的协同过滤推荐算法测试项目
* 类 名 称 ：Demo
* 类 描 述 ：基于物品的协同过滤推荐算法测试类
* 所在的域 ：LDS
* 命名空间 ：RecommendLDSDemo
* 机器名称 ：LDS 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：李东升
* 创建时间 ：2019/12/21 9:32:26
* 更 新 人 ：李东升
* 更新时间 ：2019/12/21 11:00:02
* 版 本 号 ：v1.0.0.0
* 个人博客 ：www.b0c0.com
* 电子邮箱 ：lidongshenglife@163.com
*
*******************************************************************
* Copyright @ 李东升 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using RecommendLDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RecommendLDSDemo
{
    class Demo
    {

        /// <summary>
        /// 构建一个datatable（大家可以直接连接数据库），
        /// 作为样例在此我就直接手动构建测试数据了（和博客中的测试数据相同）。
        /// </summary>
        /// <returns></returns>
        public DataTable CreatDataTable()
        {

            DataTable tblDatas = new DataTable("Datas");
            DataColumn dc = null;
            dc = tblDatas.Columns.Add("userId", Type.GetType("System.Int32"));
            dc = tblDatas.Columns.Add("goodId", Type.GetType("System.Int32"));
            dc = tblDatas.Columns.Add("rating", Type.GetType("System.Double"));
            int[][] data = new int[5][];
            data[0] = new int[] { 2, 15, 39, 57 };
            data[1] = new int[] { 6, 24, 57, 60 };
            data[2] = new int[] { 20, 15, 39, 60 };
            data[3] = new int[] { 31, 15, 24 };
            data[4] = new int[] { 35, 39, 41, 60 };

            DataRow newRow;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < data[i].Length; j++)
                {
                    newRow = tblDatas.NewRow();
                    //用户id
                    newRow[0] = data[i][0];
                    //物品id
                    newRow[1] = data[i][j];
                    //兴趣度
                    newRow[2] = 1;
                    tblDatas.Rows.Add(newRow);
                }
            }
            this.OutPutDataTable(tblDatas);
            return tblDatas;
        }

        /// <summary>
        /// 输出指定的DataTable
        /// </summary>
        /// <param name="dataTable"></param>
        public void OutPutDataTable(DataTable dataTable)
        {
            Console.WriteLine("================dataTable================");
            Console.WriteLine("");
            Console.WriteLine("userId   goodId    rating");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Console.WriteLine(dataTable.Rows[i][0] + "          " + dataTable.Rows[i][1] + "          " + dataTable.Rows[i][2]);
            }
            Console.WriteLine("");
            Console.WriteLine("================dataTable================");
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        public void Run()
        {
            //第二个参数为是否打开控制台输出调试日志
            RecommendBaseGood recommendBaseGood = new RecommendBaseGood(this.CreatDataTable(), true);
            //所有用户的预测兴趣度 
            Dictionary<int, Dictionary<int, double>> allUser = recommendBaseGood.Get_SimilarityAllUser();

            foreach (KeyValuePair<int, Dictionary<int, double>> keyValuePair in allUser)
            {
                Console.WriteLine("");
                Console.WriteLine("=============为userId:" + keyValuePair.Key + "推荐=============");

                Console.WriteLine("goodId   推荐指数");
                foreach (KeyValuePair<int, double> similarityGoods in keyValuePair.Value)
                {
                    Console.WriteLine(similarityGoods.Key + "   " + similarityGoods.Value);
                }
            }
        }
    }
}
