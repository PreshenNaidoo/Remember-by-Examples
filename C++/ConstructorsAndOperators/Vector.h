#pragma once
#include <iostream>

using namespace std;

class Vector
{
	friend void PrintVector(Vector vec);
	friend Vector MakeUnit(const Vector &vec);
	friend ostream &operator<<(ostream &os, const Vector &vec);

private:
	double X = 0;
	double Y = 0;
	double Z = 0;

public:

	Vector(double x, double y, double z);
	Vector();

	//Copy constructor
	Vector(const Vector &vec);

	~Vector();

	//Assignmnet
	Vector &operator=(const Vector &rhs);
	//Arithmetic
	Vector operator-(const Vector &rhs) const;
	Vector operator+(const Vector &rhs) const;
	Vector operator*(const Vector &rhs) const;
	Vector operator/(const Vector &rhs) const;
	//relational
	bool operator==(const Vector &rhs) const;
	bool operator<(const Vector &rhs) const;
	bool operator>(const Vector &rhs) const;
	//Stream
	//Should be global. Because LHS should be ouput stream
	//std::ostream &operator<<(const std::ostream &os,const Vector &vec);

	double Get_Length() const;

};

