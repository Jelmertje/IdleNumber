using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNumber
{
    private float number; //The value
    private int scale; //The scale modifier of the number

    private int displayResolution; //The number of significant digits to display
    private int dataResolution; //The number of significant digits to store

    string[] suffixes = { "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

    //Unique identifier
    private readonly int id;

    //Constructors
    public IdleNumber() : this(4, 18) { }

    public IdleNumber(int displayResolution, int dataResolution, float startingValue = 0f, int startingScale = 0)
    {
        number = startingValue;
        this.displayResolution = displayResolution;
        this.dataResolution = dataResolution;
        scale = startingScale;
        Res();

        id = System.Guid.NewGuid().GetHashCode();
    }

    public IdleNumber(IdleNumber idleN)
    {
        this.number = idleN.GetNumber();
        this.scale = idleN.GetScale();
        this.displayResolution = idleN.GetDisplayResolution();
        this.dataResolution = idleN.GetDataResolution();

        id = System.Guid.NewGuid().GetHashCode();
    }

    //Manipulation with operators (+)
    public static IdleNumber operator +(IdleNumber n1, IdleNumber n2)
    {
        IdleNumber idleN;
        if (n1.GetScale() >= n2.GetScale())
        {
            idleN = new IdleNumber(n1);
            idleN.Add(n2.GetNumber() / Mathf.Pow(10f, n1.GetScale() - n2.GetScale()));
        }
        else
        {
            idleN = new IdleNumber(n2);
            idleN.Add(n1.GetNumber() / Mathf.Pow(10f, n2.GetScale() - n1.GetScale()));
        }

        return idleN;
    }
    public static IdleNumber operator +(IdleNumber n1, float f)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Add(f);

        return idleN;
    }
    public static IdleNumber operator +(IdleNumber n1, int i)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Add(i);

        return idleN;
    }

    //Manipulation with operators (-)
    public static IdleNumber operator -(IdleNumber n1, IdleNumber n2)
    {
        IdleNumber idleN;
        if (n1.GetScale() >= n2.GetScale())
        {
            idleN = new IdleNumber(n1);
            idleN.Subtract(n2.GetNumber() / Mathf.Pow(10f, n1.GetScale() - n2.GetScale()));
        }
        else
        {
            idleN = new IdleNumber(n2);
            idleN.Subtract(n1.GetNumber() / Mathf.Pow(10f, n2.GetScale() - n1.GetScale()));
        }

        return idleN;
    }
    public static IdleNumber operator -(IdleNumber n1, float f)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Subtract(f);

        return idleN;
    }
    public static IdleNumber operator -(IdleNumber n1, int i)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Subtract(i);

        return idleN;
    }
    //Manipulation with operators (*)
    public static IdleNumber operator *(IdleNumber n1, IdleNumber n2)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Multiply(n2.GetNumber());
        idleN.SetScale(idleN.GetScale() + n2.GetScale());

        return idleN;
    }
    public static IdleNumber operator *(IdleNumber n1, float f)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Multiply(f);

        return idleN;
    }
    public static IdleNumber operator *(IdleNumber n1, int i)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Multiply(i);

        return idleN;
    }
    //Manipulation with operators (/)
    public static IdleNumber operator /(IdleNumber n1, IdleNumber n2)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Divide(n2.GetNumber());
        idleN.SetScale(idleN.GetScale() - n2.GetScale());

        return idleN;
    }
    public static IdleNumber operator /(IdleNumber n1, float f)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Divide(f);

        return idleN;
    }
    public static IdleNumber operator /(IdleNumber n1, int i)
    {
        IdleNumber idleN = new IdleNumber(n1);
        idleN.Divide(i);

        return idleN;
    }

    //Manipulating with functions (float)
    public void Add(float value)
    {
        number += value / Mathf.Pow(10f, scale);
        Res();
    }
    public void Add(int value)
    {
        number += value / Mathf.Pow(10f, scale);
        Res();
    }
    public void Subtract(float value)
    {
        number -= value / Mathf.Pow(10f, scale);
        Res();
    }
    public void Subtract(int value)
    {
        number -= value / Mathf.Pow(10f, scale);
        Res();
    }
    public void Multiply(float value)
    {
        number *= value;
        Res();
    }
    public void Multiply(int value)
    {
        number *= value;
        Res();
    }
    public void Divide(float value)
    {
        Multiply(1f / value);
    }
    public void Divide(int value)
    {
        Multiply(1f / value);
    }

    //Apply resolution, this is a function that should be called after every manipulation.
    //The functions Add, Subtract and Multply call this and every other manipulation function calls one of those three
    private void Res()
    {
        while (number > Mathf.Pow(10, dataResolution))
        {
            Debug.Log("Res");
            number /= 10f;
            scale++;
        }
    }

    //Comparison operators
    //Greater than >
    public static bool operator >(IdleNumber n1, IdleNumber n2){
        if(n1.GetScale() >= n2.GetScale()){
            if(n1.GetScale() == n2.GetScale()){
                return n1.GetNumber() > n2.GetNumber();
            }
            return true;
        }
        return false;
    }
    public static bool operator >(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 > n2;
    }
    public static bool operator >(IdleNumber n1, int i){
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 > n2;
    }

    //Smaller than <
    public static bool operator <(IdleNumber n1, IdleNumber n2)
    {
        if (n1.GetScale() <= n2.GetScale())
        {
            if (n1.GetScale() == n2.GetScale())
            {
                return n1.GetNumber() < n2.GetNumber();
            }
            return true;
        }
        return false;
    }
    public static bool operator <(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 < n2;
    }
    public static bool operator <(IdleNumber n1, int i)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 < n2;
    }

    //Greater than or equal to >=
    public static bool operator >=(IdleNumber n1, IdleNumber n2)
    {
        return !(n1 < n2);
    }
    public static bool operator >=(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 >= n2;
    }
    public static bool operator >=(IdleNumber n1, int i)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 >= n2;
    }

    //Smaller than or equal to <=
    public static bool operator <=(IdleNumber n1, IdleNumber n2)
    {
        return !(n1 > n2);
    }
    public static bool operator <=(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 <= n2;
    }
    public static bool operator <=(IdleNumber n1, int i)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 <= n2;
    }

    //Equal to
    public static bool operator ==(IdleNumber n1, IdleNumber n2)
    {
        return n1.GetScale() == n2.GetScale() && System.Math.Abs(n1.GetNumber() - n2.GetNumber()) < float.Epsilon;
    }
    public static bool operator ==(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 == n2;
    }
    public static bool operator ==(IdleNumber n1, int i)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 == n2;
    }

    //Not equal to
    public static bool operator !=(IdleNumber n1, IdleNumber n2)
    {
        return !(n1 == n2);
    }
    public static bool operator !=(IdleNumber n1, float f)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), f);
        return n1 != n2;
    }
    public static bool operator !=(IdleNumber n1, int i)
    {
        IdleNumber n2 = new IdleNumber(n1.GetDisplayResolution(), n1.GetDisplayResolution(), (float)i);
        return n1 != n2;
    }

    //Serves up a readable string
    public string GetAsString()
    {
        float tNumber = number;
        int mille = -1 + scale;
        int displayCrop = 0;

        while (tNumber >= 1000f)
        {
            tNumber /= 1000f;
            mille++;
        }

        //Correction to assure total amount of digits displayed
        float tNumber2 = tNumber;
        while (tNumber2 >= 10f){
            tNumber2 /= 10f;
            displayCrop++;
        }

        if (mille == -1)
            return tNumber.ToString("f" + (displayResolution - displayCrop - 1));
        else if (mille < suffixes.Length)
            return tNumber.ToString("f" + (displayResolution - displayCrop - 1)) + " " + suffixes[mille];
        else
            return tNumber.ToString("f" + (displayResolution - displayCrop - 1)) + " e" + (mille + 1) * 3;
    }

    //getters 'n setters
    public float GetNumber()
    {
        return number;
    }
    public void SetNumber(float number){
        this.number = number;
    }
    public void SetNumber(int number)
    {
        this.number = (float)number;
    }
    public int GetScale()
    {
        return scale;
    }
    public void SetScale(int scale)
    {
        this.scale = scale;
    }
    public int GetDisplayResolution()
    {
        return displayResolution;
    }
    public int GetDataResolution()
    {
        return dataResolution;
    }
    public override string ToString()
    {
        return number + "x" + scale;
    }

    //Important overrides
    public override bool Equals(object obj)
    {
        var idleN = obj as IdleNumber;
        if(idleN == null) {
            return false;
        }
        return this.id == idleN.id;
    }
    public override int GetHashCode()
    {
        return id;
    }
}
