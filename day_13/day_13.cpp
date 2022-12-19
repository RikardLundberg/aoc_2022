#include <iostream>
#include <string>
#include <vector>

void first();
void second();

int main()
{
	first();
	//second();
}

class package {
public:
	std::vector<package> packages;
	int integer = -1;
	std::string packageStr;
};

package readPackage(std::string packageStr)
{
	int openBracketCount = 0;
	package currPackage;
	if (packageStr == "")
	{
		package nullPackage;
		currPackage.packages.push_back(nullPackage);
	}

	int indexStartPack = 0;
	std::string number = "";
	std::string subPackage = "z";

	for (int i = 0; i < packageStr.length(); i++) {
		if (number != "" && packageStr[i] != '[' && packageStr[i] != ']' && packageStr[i] != ',')
		{
			number += packageStr[i];
			continue;
		}
		if (number != "")
		{
			package nbrPackage;
			nbrPackage.integer = std::stoi(number);
			currPackage.packages.push_back(nbrPackage);
			number = "";
		}

		if (packageStr[i] == '[')
		{
			openBracketCount++;
			if (subPackage == "z") {
				subPackage = "";
				continue;
			}
		}
		else if (packageStr[i] == ']')
		{
			openBracketCount--;
			if (openBracketCount == 0) {
				currPackage.packages.push_back(readPackage(subPackage));
				subPackage = "z";
			}
		}
		else if (subPackage == "z" && packageStr[i] != ',')
		{
			number += packageStr[i];
		}
		if (subPackage != "z")
			subPackage += packageStr[i];
	}
	if (number != "")
	{
		package nbrPackage;
		nbrPackage.integer = std::stoi(number);
		currPackage.packages.push_back(nbrPackage);
		number = "";
	}
	currPackage.packageStr = packageStr;
	return currPackage;
}

int comparePackages(package pack1, package pack2, bool firstCheck)
{
	int returnResult = 0;
	if (pack1.packages.size() == 0 && pack2.packages.size() == 0)
	{
		if (pack2.integer < pack1.integer)
			return -1;
		else if (pack2.integer == pack1.integer)
			return 0;
		else
			return 1;
	}
	else if (pack1.packages.size() > 0 && pack2.packages.size() == 0)
	{
		if (pack2.integer == -1)
			return -1;
		package tmpPackage;
		tmpPackage.integer = pack2.integer;
		returnResult = comparePackages(pack1.packages[0], tmpPackage, firstCheck);
		if (returnResult != 1)
			return -1;

	}
	else if (pack1.packages.size() == 0 && pack2.packages.size() > 0)
	{
		package tmpPackage;
		tmpPackage.integer = pack1.integer;
		returnResult = comparePackages(tmpPackage, pack2.packages[0], firstCheck);
		if (firstCheck && returnResult != -1)
			return 1;
	}
	else {
		for (int i = 0; i < pack1.packages.size(); i++)
		{
			if (pack2.packages.size() == i)
				return -1;
			returnResult = comparePackages(pack1.packages[i], pack2.packages[i], firstCheck);
			if (returnResult != 0)
				break;
		}
	}
	return returnResult;
}

void first()
{
	std::string input, secondInput;
	std::vector<package> packages;

	while (std::cin >> input && input != "eof")
	{
		package currPackage = readPackage(input.substr(1, input.length() - 2));
		packages.push_back(currPackage);
	}

	int correctIndecesSum = 0;
	int index = 0;
	for (int i = 0; i < packages.size(); i++)
	{
		index++;
		auto firstPackage = packages[i];
		auto secondPackage = packages[++i];
		if (-1 != comparePackages(firstPackage, secondPackage, true))
			correctIndecesSum += index;
	}

	std::cout << "CorrectIndecesSum: " << correctIndecesSum;
}

void second()
{
	std::string input, secondInput;
	std::vector<package> packages;

	while (std::cin >> input && input != "eof")
	{
		package currPackage = readPackage(input.substr(1, input.length() - 2));
		packages.push_back(currPackage);
	}

	package div1 = readPackage("[[2]]");
	package div2 = readPackage("[[6]]");

	int div1Sum = 1, div2Sum = 2;
	std::vector<std::string> div1small;

	for (package package : packages) {
		if (-1 != comparePackages(package, div1, false)) {
			div1Sum++;
			div1small.push_back(package.packageStr);
		}
		if (-1 != comparePackages(package, div2, false))
			div2Sum++;
	}
	int decoderKey = div1Sum * div2Sum;
	std::cout << "Decoder key: " << decoderKey;
}