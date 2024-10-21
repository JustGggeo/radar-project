using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class testing : MonoBehaviour
{
    public static double x, y, z, d, phi;
    public static void Main(string[] args)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "test.txt");

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                string[] values = line.Split(' ');

                x = Convert.ToDouble(values[0]);
                y = Convert.ToDouble(values[1]);
                z = Convert.ToDouble(values[2]);
                d = Convert.ToDouble(values[3]);
                phi = Convert.ToDouble(values[4]);
            }

            
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"���� '{filePath}' �� ������.");
        }
        catch (FormatException e)
        {
            Console.WriteLine($"������������ ������ ����� � �����.");
        }
        catch (IOException e)
        {
            Console.WriteLine($"��������� ������ ��� ������ �����: {e.Message}");
        }


    }

    public static void CalcCoords()//�������� (� �����)
    {

        double distance = Main.d;
        double angle = phi * Math.PI / 180.0; // ��������� ������� � �������
        Vector3 location = new Vector3(x, y, z);
        Vector3 locatorPosition = location;

        // �������������� � �����������
        Vector3 direction = new Vector3(locatorPosition.X + distance * Math.Cos(angle), locatorPosition.Y + distance * Math.Sin(angle), locatorPosition.Z);

        // �������������� � ��������� ����������
        Vector3 position = direction;

    }

}