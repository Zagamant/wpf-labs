﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;


namespace Gr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeometryModel3D mGeometry; //геом модель которую будем рисовать 
        private bool mDown; //переменная для проверки нажатия левой кнопки мыши
        private Point mLastPos; //переменная для работы с камерой

        double a = 1, b = 3;


        public MainWindow()
        {
            InitializeComponent(); //инициализация программы
            BuildSolid(); //вывов функции которая будет рисовать  
        }

        private void BuildSolid()
        {

            // double a = Double.Parse(aValue.Text);

            //треугольник

            //MeshGeometry3D mesh = new MeshGeometry3D(); // создаем сетку на основе которой будет создана модель
            // //добавляем вершины сетки
            //mesh.Positions.Add(new Point3D(-1, -1, 1)); //0
            //mesh.Positions.Add(new Point3D(1, -1, 1)); //1
            //mesh.Positions.Add(new Point3D(1, 1, 1)); //2

            ////создаем стороны куба из треугольников 
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2);

            //mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Green)); //создаем модель из сетки
            //mGeometry.Transform = new Transform3DGroup(); //создаем трансформацию для модели
            //group.Children.Add(mGeometry); //группируем модель(если много для них освещение)



            //куб

            //MeshGeometry3D mesh = new MeshGeometry3D(); // создаем сетку на основе которой будет создана модель
            //mesh.Positions.Add(new Point3D(-1, -1, 1)); //0
            //mesh.Positions.Add(new Point3D(1, -1, 1)); //1
            //mesh.Positions.Add(new Point3D(1, 1, 1)); //2
            //mesh.Positions.Add(new Point3D(-1, 1, 1)); //3
            //mesh.Positions.Add(new Point3D(-1, -1, -1)); //4
            //mesh.Positions.Add(new Point3D(1, -1, -1)); //5
            //mesh.Positions.Add(new Point3D(1, 1, -1)); //6
            //mesh.Positions.Add(new Point3D(-1, 1, -1)); //7

            //////создаем стороны куба из треугольников 
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); //спереди
            //mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(0);
            //mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(6); //справа
            //mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(2);
            //mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(6); //сверху
            //mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(7);
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(1); //снизу
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(5);
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(7); //слева
            //mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(4);
            //mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(5); //сзади
            //mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(4);

            //mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Green)); //создаем модель из сетки
            //mGeometry.Transform = new Transform3DGroup(); //создаем трансформацию для модели
            //group.Children.Add(mGeometry); //группируем модель(если много для них освещение)



            //    //поверхность

            double[] aryForAxesX = new double[100]; //массив для точек по x
            double[] aryForAxesY = new double[100]; //массив для точек по y
            double[] aryForAxesZ = new double[10000]; //массив для поверхности

            for (int i = 0; i < 100; i++)
            {
                //рассчитываем массив для точек по x
                aryForAxesX[i] = (double)1 / 100 * i - 0.5;
            }
            //копируем массив x в массив y
            aryForAxesY = aryForAxesX;

            for (int i = 0; i < aryForAxesX.Length; i++)
            {
                for (int j = 0; j < aryForAxesY.Length; j++)
                {
                    //по уравнению поверхности рассчитываем массив для поверхности
                    // aryForAxesZ[i * 100 + j] = Math.Sqrt(aryForAxesX[i] * aryForAxesX[i] + aryForAxesY[j] * aryForAxesY[j]);
                    aryForAxesZ[i * 100 + j] = ((aryForAxesX[i] * aryForAxesX[i]) / (a * a) - (2 * aryForAxesY[j]) / (b * b));
                }
            }

            MeshGeometry3D mesh = new MeshGeometry3D(); //создаем сетку для создания модели
            double radius = 0.005; //половина длины ребра куба или радиус

            for (int i = 0; i < aryForAxesZ.Length; i++)
            {
                //Добавляем вершины сетки
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] - radius, aryForAxesY[i % 100] - radius, aryForAxesZ[i] + radius)); //0
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] + radius, aryForAxesY[i % 100] - radius, aryForAxesZ[i] + radius)); //1
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] + radius, aryForAxesY[i % 100] + radius, aryForAxesZ[i] + radius)); //2
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] - radius, aryForAxesY[i % 100] + radius, aryForAxesZ[i] + radius)); //3
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] - radius, aryForAxesY[i % 100] - radius, aryForAxesZ[i] - radius)); //4
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] + radius, aryForAxesY[i % 100] - radius, aryForAxesZ[i] - radius)); //5
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] + radius, aryForAxesY[i % 100] + radius, aryForAxesZ[i] - radius)); //6
                mesh.Positions.Add(new Point3D(aryForAxesX[i / 100] - radius, aryForAxesY[i % 100] + radius, aryForAxesZ[i] - radius)); //7

                //создаем стороны куба из треугольников
                mesh.TriangleIndices.Add(i * 8 + 0); mesh.TriangleIndices.Add(i * 8 + 1); mesh.TriangleIndices.Add(i * 8 + 2); //спереди
                mesh.TriangleIndices.Add(i * 8 + 0); mesh.TriangleIndices.Add(i * 8 + 2); mesh.TriangleIndices.Add(i * 8 + 3);
                mesh.TriangleIndices.Add(i * 8 + 1); mesh.TriangleIndices.Add(i * 8 + 5); mesh.TriangleIndices.Add(i * 8 + 6); //справа
                mesh.TriangleIndices.Add(i * 8 + 1); mesh.TriangleIndices.Add(i * 8 + 6); mesh.TriangleIndices.Add(i * 8 + 2);
                mesh.TriangleIndices.Add(i * 8 + 2); mesh.TriangleIndices.Add(i * 8 + 6); mesh.TriangleIndices.Add(i * 8 + 7); //сверху
                mesh.TriangleIndices.Add(i * 8 + 2); mesh.TriangleIndices.Add(i * 8 + 7); mesh.TriangleIndices.Add(i * 8 + 3);
                mesh.TriangleIndices.Add(i * 8 + 4); mesh.TriangleIndices.Add(i * 8 + 5); mesh.TriangleIndices.Add(i * 8 + 1); //снизу
                mesh.TriangleIndices.Add(i * 8 + 4); mesh.TriangleIndices.Add(i * 8 + 1); mesh.TriangleIndices.Add(i * 8 + 0);
                mesh.TriangleIndices.Add(i * 8 + 5); mesh.TriangleIndices.Add(i * 8 + 4); mesh.TriangleIndices.Add(i * 8 + 7); //слева
                mesh.TriangleIndices.Add(i * 8 + 5); mesh.TriangleIndices.Add(i * 8 + 7); mesh.TriangleIndices.Add(i * 8 + 6);
                mesh.TriangleIndices.Add(i * 8 + 4); mesh.TriangleIndices.Add(i * 8 + 0); mesh.TriangleIndices.Add(i * 8 + 3); //сзади
                mesh.TriangleIndices.Add(i * 8 + 4); mesh.TriangleIndices.Add(i * 8 + 3); mesh.TriangleIndices.Add(i * 8 + 7);
            }

            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Coral)); //создаем модель из сетки
            mGeometry.Transform = new Transform3DGroup(); //создаем трансформацию для модели
            group.Children.Add(mGeometry); //группируем модель(если много для них освещение)

        }


        //обрабатываем нажатие мыши
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            mDown = true;
            Point pos = Mouse.GetPosition(viewport);
            mLastPos = new Point(pos.X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - pos.Y);
        }

        //движение модели при нажатой левой кнопке мыши и передвижении мыши
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mDown)
            {
                Point pos = Mouse.GetPosition(viewport);
                Point actualPos = new Point(pos.X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - pos.Y);
                double dx = actualPos.X - mLastPos.X, dy = actualPos.Y - mLastPos.Y;

                double mouseAngle = 0;
                if (dx != 0 && dy != 0)
                {
                    mouseAngle = Math.Asin(Math.Abs(dy) / Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)));
                    if (dx < 0 && dy > 0) mouseAngle += Math.PI / 2;
                    else if (dx < 0 && dy < 0) mouseAngle += Math.PI;
                    else if (dx > 0 && dy < 0) mouseAngle += Math.PI * 1.5;
                }

                else if (dx == 0 && dy != 0) mouseAngle = Math.Sign(dy) > 0 ? Math.PI / 2 : Math.PI * 1.5;
                else if (dx != 0 && dy == 0) mouseAngle = Math.Sign(dx) > 0 ? 0 : Math.PI;

                double axisAngle = mouseAngle + Math.PI / 2;

                Vector3D axis = new Vector3D(Math.Cos(axisAngle) * 4, Math.Sin(axisAngle) * 4, 0);
                double rotation = 0.01 * Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

                Transform3DGroup group = mGeometry.Transform as Transform3DGroup;
                QuaternionRotation3D r = new QuaternionRotation3D(new Quaternion(axis, rotation * 180 / Math.PI));
                group.Children.Add(new RotateTransform3D(r));
                mLastPos = actualPos;
            }
        }

        //отдаление камеры при прокрутке мыши
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - e.Delta / 500D);
        }

        // обработка прекращения нажатия мыши
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mDown = false;
        }
    }
}
