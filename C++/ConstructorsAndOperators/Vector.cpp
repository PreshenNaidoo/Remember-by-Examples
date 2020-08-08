#include "Vector.h"
#include <iostream>
#include <math.h>

using namespace std;

Vector::Vector(double x, double y, double z)
	:X{ x }, Y{ y }, Z{ z }
{
	cout << "In primary constructor" << endl;
}

Vector::Vector()
	: Vector(0, 0, 0)
{
	cout << "Delegate constructor" << endl;
}

Vector::Vector(const Vector &vec)
{
	this->X = vec.X;
	this->Y = vec.Y;
	this->Z = vec.Z;

	cout << "In copy constructor" << endl;
}

Vector::~Vector()
{
	cout << "In destructor" << endl;
}

Vector &Vector::operator=(const Vector &rhs)
{
	if (this == &rhs)
		return *this;

	this->X = rhs.X;
	this->Y = rhs.Y;
	this->Z = rhs.Z;

	return *this;
}

Vector Vector::operator+(const Vector &rhs) const
{
	double x = this->X + rhs.X;
	double y = this->Y + rhs.Y;
	double z = this->Z + rhs.Z;

	return Vector(x, y, z);
}

Vector Vector::operator-(const Vector &rhs) const
{
	double x = this->X - rhs.X;
	double y = this->Y - rhs.Y;
	double z = this->Z - rhs.Z;

	return Vector(x, y, z);
}

Vector Vector::operator*(const Vector &rhs) const
{
	double x = this->X * rhs.X;
	double y = this->Y * rhs.Y;
	double z = this->Z * rhs.Z;

	return Vector(x, y, z);
}

Vector Vector::operator/(const Vector &rhs) const
{
	double x = this->X / rhs.X;
	double y = this->Y / rhs.Y;
	double z = this->Z / rhs.Z;

	return Vector(x, y, z);
}

bool Vector::operator==(const Vector &rhs) const
{
	return X == rhs.X && Y == rhs.Y && Z == rhs.Z;
}

bool Vector::operator<(const Vector &rhs) const
{
	return Get_Length() < rhs.Get_Length();
}

bool Vector::operator>(const Vector &rhs) const
{
	return Get_Length() > rhs.Get_Length();
}

double Vector::Get_Length() const
{
	return sqrt(pow(X, 2) + pow(Y, 2) + pow(Z, 2));
}
