// LinkedListTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

using namespace std;


template <class Type>
class Node
{
public:
	Type Value;
	Node<Type> *Link = nullptr;
};

template <class Type>
class LinkedListIterator
{
public:
	LinkedListIterator() {}

	LinkedListIterator(Node<Type>* node)
		:current{ node }
	{
	}

	//Overload thr dereferencing operator
	Type operator*()
	{
		return current->Value;
	}

	LinkedListIterator<Type> operator++()
	{
		current = current->Link;
		return *this;
	}

	bool operator==(const LinkedListIterator& rhs) const
	{
		return *this->current == rhs.current;
	}

	bool operator!=(const LinkedListIterator& rhs) const
	{
		return current != rhs.current;
	}

private:
	Node<Type>* current = nullptr;

	//Note: All the functions defined above are of O(1)
};

template <class Type>
class LinkedList
{
public:
	LinkedList() {};

	//Copy Constructor
	LinkedList(const LinkedList& list)
	{
		CopyList(list);
	}

	~LinkedList()
	{
		Clear();
	}

	const LinkedList<Type> &operator=(const LinkedList<Type>& rhs)
	{
		if (this == &rhs)
			return *this;

		CopyList(rhs);
		return *this;
	}

	bool IsEmpty() const
	{
		return Count == 0;
	}

	void Print() const
	{
		Node<Type> current{ First };
		while (current != nullptr)
		{
			cout << current->Value << " " << endl;
			current = current->Link;
		}
	}

	int Length() const
	{
		return Count;
	}

	void Clear()
	{
		Node<Type> *temp;
		while (First != nullptr)
		{
			temp = First;
			First = First->Link;
			delete temp;
		}

		Last = nullptr;
		Count = 0;
	}

	Type Front() const
	{
		if (IsEmpty())
			return Type{};	//Forces all Types to have a default constructor

		return *First->Value;
	}

	Type Back() const
	{
		if (IsEmpty())
			return Type{};

		return *Last->Value;
	}

	virtual bool Find(const Type& val) = 0;
	virtual void InsertFirst(const Type& val) = 0;
	virtual void Add(const Type& val) = 0;
	virtual void Remove(const Type& val) = 0;

	LinkedListIterator<Type> Begin()
	{
		return LinkedListIterator{ First };
	}

	LinkedListIterator<Type> End()
	{
		return LinkedListIterator{ nullptr };
	}

protected:
	int Count = 0;
	Node<Type>* First = nullptr;
	Node<Type>* Last = nullptr;

private:
	void CopyList(const LinkedList<Type> &list)
	{
		Node<Type>* newNode;
		Node<Type>* current;

		if (Count != 0) //Empty the current list.
			Clear();

		if (list->IsEmpty())
		{
			return;
		}

		current = list->First;

		First = new Node<Type>;
		First->Link = nullptr;
		First->Value = current->Value;
		Last = First;
		current = current->Link;

		while (current != nullptr)
		{
			newNode = new Node<Type>;
			newNode->Value = current->Value;
			newNode = nullptr;
			Last->Link = newNode;
			Last = newNode;
			current = current->Link;
		}
	}
};

template <class Type>
class UnorderedList: public LinkedList<Type>
{
public:
	bool Find(const Type& val)
	{
		Node<Type>* current = UnorderedList::First;
		bool found{ false };

		while (current != nullptr && !found)
		{
			if (current->Value == val)
				found = true;
		}

		return found;
	}

	void InsertFirst(const Type& val) override
	{
		Node<Type>* newNode = new Node<Type>;
		newNode->Link = UnorderedList::First;
		newNode->Value = val;
		UnorderedList::First = newNode;
		UnorderedList::Count++;
		
		if (UnorderedList::Last == nullptr)	//If this list only has 1 item then the last node is also this item.
			UnorderedList::Last = newNode;
	}

	void Add(const Type& val) override
	{
		Node<Type>* newNode = new Node<Type>;
		newNode->Value = val;
		newNode->Link = nullptr;

		if (UnorderedList::First == nullptr)
		{
			UnorderedList::First = newNode;
			UnorderedList::Last = newNode;
			UnorderedList::Count++;
		}
		else
		{
			UnorderedList::Last->Link = newNode;
			UnorderedList::Last = newNode;
			UnorderedList::Count++;
		}
	}

	void Remove(const Type& val) override
	{
		Node<Type>* current;
		Node<Type>* trailCurrent;
		bool found = false;

		if (UnorderedList::First == nullptr)
			return;

		if (UnorderedList::First->Value == val)
		{
			current = UnorderedList::First;
			UnorderedList::First = UnorderedList::First->Link;
			UnorderedList::Count--;

			if (UnorderedList::First == nullptr)
				UnorderedList::Last = nullptr;

			delete current;
		}
		else
		{
			//Search list
			trailCurrent = UnorderedList::First;
			current = UnorderedList::First->Link;
			while (current != nullptr && !found)
			{
				if (current->Value != val)
				{
					trailCurrent = current;
					current = current->Link;
				}
				else
					found = true;
			}

			if (found)
			{
				trailCurrent->Link = current->Link;
				UnorderedList::Count--;
				if (UnorderedList::Last == current)	//It was the last node to be deleted.
					UnorderedList::Last = trailCurrent;
				delete current;
			}
		}
	}
};

int main()
{
	UnorderedList<int> *list = new UnorderedList<int>;
	std::cout << "Hello World!\n";
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
