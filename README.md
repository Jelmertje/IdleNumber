# IdleNumber
IdleNumber class in C# for displaying crazy high numbers you see in idle clicker games

IdleNumber is a class that essentially stores 2 values, 1 float for your significant digits, and an int that scales this float to a power of 10.

I wrote IdleNumber as a generic solution to working with the insane values a lot of idle clicker games use. Idle number implements operator overloading so you can add, subtract, multiply and divide them with eachother or with an int/float.
It is also possible to compare them in the same way.

Examples:
```c#
IdleNumber idleN1 = new IdleNumber();

idleN1 += 2; //IdleN1 is now 2
idleN1 = idleN1 * 8.5f; //IdleN1 is now 17

idleN1 > 0; //true

IdleNumber idleN2 = new IdleNumber();

idleN2 += 4;
idleN2 > idleN1; //false

idleN1 *= idleN2; //idleN1 is now 68
```
