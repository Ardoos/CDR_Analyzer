# CDR-Analyzer

Program s�u�y do przetwarzania i wyszukiwania informacji na temat po��cze� telefonicznych z central, tzw. CDR.
Aplikacja umo�liwia przechowywanie danych zar�wno w bazie danych jak i w li�cie dzi�ki czemu mo�na por�wna� szybko�� dzia�ania tych dw�ch sposob�w.

## Wymagania wst�pne
Projekt tworzony by� z u�yciem Visual Studio 2017 i korzysta z niereleacyjnej bazy danych MongoDB. Program mo�e by� edytowany w dowolnym IDE, operacja na listach mog� by� wykonywane nawet bez zainstalowanej bazy danych.

## Ustawienie projektu
- W pliku 'Program.cs', na pocz�tku funkcji main nale�y u�y� funkcji 'SetFilePath' stworzonego wcze�niej 'ParserController', aby ustawi� prawid�ow� �cie�k� z danymi np. 'parserController.SetFilePath("..\\..\\cdr_small.txt");''
- Aby umo�liwi� dzia�anie na bazie danych MongoDB nale�y wykona� nast�puj�ce operacje:
  * Zainstalowa� *Community Server* [MongoDB](https://www.mongodb.com/download-center?#community)
  * Stworzy� baz� danych poleceniem 'use CDR_Data'
  * B�d�c prze��czonym na now� baz� danych, stworzy� kolekcj� poleceniem 'db.createCollection("CallRecords")'
Ewentualne rozbie�no�ci co do nazw nale�y skorygowa� w pliku 'DB.cs'

## Korzystanie z programu
- Program daje mo�liwo�� przetwarzania danych w bazie danych MongoDB (domy�lnie) lub na listach, �r�d�o tych danych mo�na zmienia� w trakcie dzia�ania programu
- Mo�liwe operacje filtrowania:
  * Po numerze telefonu dzwoni�cego, po wybraniu tej opcji nale�y wpisa� 11 cyfrowy numer
  * Po numerze telefonu odbieraj�cego, po wybraniu tej opcji nale�y wpisa� 11 cyfrowy numer
  * Po dacie rozpocz�cia po��czenia, wymagany jest jeden z operator�w *> = <*, nast�pnie data i opcjonalnie godzina np. '> 11/01/2017 17:05'
  * Po dacie zako�czenia po��czenia, wymagany jest jeden z operator�w *> = <*, nast�pnie data i opcjonalnie godzina np. '> 11/01/2017 17:05'
  * Po rodzaju po��czenia, jeden z rodzaj�w po��czenia wybierany za pomoc� cyfry 1-6
  * Po op�acie za po��czenie, nale�y poda� jeden z operatorow *> = <* oraz kwot� np. '> 25'
- Mo�liwe jest zapisanie przefiltrowanie danych do pliku, domy�lnie jest to plik 'filtered.txt' w folderze g��wnym 'CDR_Analyzer', mo�na to zmieni� w pliku 'SaveData.cs' w linijce 'System.IO.File.WriteAllLines("..\\..\\filtered.txt", lines);'
- Dzia�anie na przefiltrowanych danych jest mo�liwe, bez wzgl�du na �r�d�o danych odbywa si� ono na listach, aby nie by�o mo�liwo�ci nadpisania pierwotnych danych w bazie
- W ka�dym momencie mo�liwe jest ponowne wczytanie danych z pliku

## Autorzy
Adrian Sondej - [Ardoos](https://github.com/Ardoos)

## Licencja
Projekt dost�pny jest na licencji MIT, sprawd� [LICENSE.MD](LICENSE.md), aby dowiedzie� si� wszystkich szczeg��w

## �r�d�o danych CDR
- Program korzysta z [pliku z danymi CDR](CDR_Analyzer/cdr_small.txt) stworzonym przez [generator](https://github.com/mayconbordin/cdr-gen)