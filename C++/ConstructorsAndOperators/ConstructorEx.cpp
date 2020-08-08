// ConstructorEx.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "Vector.h"

using namespace std;

void PrintVector(Vector vec)
{
	cout << "Vector:" << vec.X << "," << vec.Y << "," << vec.Z << endl;
}

ostream &operator<<(ostream &os, const Vector &vec)
{
	os << "Vector:" << vec.X << "," << vec.Y << "," << vec.Z << endl;
	return os;
}

Vector MakeUnit(const Vector &vec)
{
	double length = vec.Get_Length();
	double x{ 0 }, y{ 0 }, z{ 0 };

	if (length > 0)
	{
		x = vec.X / length;
		y = vec.Y / length;
		z = vec.Z / length;
	}

	Vector newVec(x, y, z);

	return newVec;
}

int main()
{
	Vector vec1{1,1,1};

	//This is not assignment. This is initialisation. Copy constructor not called here.
	//The default assignment operator(=) is called or your defined one.
	Vector vec2{ vec1 };

	//This is assignment
	Vector vec3;
	vec3 = vec2;

	//Copy constructor called here, pass by value
	PrintVector(vec3);

	//Copy constructor called here, return value
	Vector unit = MakeUnit(vec3);	

	//Test operators
	Vector vec4{ 2,2,2 };
	Vector vec5{ 4,4,4 };

	Vector vec6 = vec4 + vec5;
	Vector vec7 = vec4 - vec5;
	Vector vec8 = vec4 * vec5;
	Vector vec9 = vec4 / vec5;

	bool test1 = vec4 == vec5;
	bool test2 = vec4 > vec5;
	bool test3 = vec4 < vec5;
	//bool test4 = vec4 <= vec5;	//No overloaded operator for <=

	cout << vec4 << endl;;


    cout << "Hello World!\n";
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
