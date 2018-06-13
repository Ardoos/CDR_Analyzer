# CDR-Analyzer

Program s≥uøy do przetwarzania i wyszukiwania informacji na temat po≥πczeÒ telefonicznych z central, tzw. CDR.
Aplikacja umoøliwia przechowywanie danych zarÛwno w bazie danych jak i w liúcie dziÍki czemu moøna porÛwnaÊ szybkoúÊ dzia≥ania tych dwÛch sposobÛw.

## Wymagania wstÍpne
Projekt tworzony by≥ z uøyciem Visual Studio 2017 i korzysta z niereleacyjnej bazy danych MongoDB. Program moøe byÊ edytowany w dowolnym IDE, operacja na listach mogπ byÊ wykonywane nawet bez zainstalowanej bazy danych.

## Ustawienie projektu
- W pliku 'Program.cs', na poczπtku funkcji main naleøy uøyÊ funkcji 'SetFilePath' stworzonego wczeúniej 'ParserController', aby ustawiÊ prawid≥owπ úcieøkÍ z danymi np. 'parserController.SetFilePath("..\\..\\cdr_small.txt");''
- Aby umoøliwiÊ dzia≥anie na bazie danych MongoDB naleøy wykonaÊ nastÍpujπce operacje:
  * ZainstalowaÊ *Community Server* [MongoDB](https://www.mongodb.com/download-center?#community)
  * StworzyÊ bazÍ danych poleceniem 'use CDR_Data'
  * BÍdπc prze≥πczonym na nowπ bazÍ danych, stworzyÊ kolekcjÍ poleceniem 'db.createCollection("CallRecords")'
Ewentualne rozbieønoúci co do nazw naleøy skorygowaÊ w pliku 'DB.cs'

## Korzystanie z programu
- Program daje moøliwoúÊ przetwarzania danych w bazie danych MongoDB (domyúlnie) lub na listach, ürÛd≥o tych danych moøna zmieniaÊ w trakcie dzia≥ania programu
- Moøliwe operacje filtrowania:
  * Po numerze telefonu dzwoniπcego, po wybraniu tej opcji naleøy wpisaÊ 11 cyfrowy numer
  * Po numerze telefonu odbierajπcego, po wybraniu tej opcji naleøy wpisaÊ 11 cyfrowy numer
  * Po dacie rozpoczÍcia po≥πczenia, wymagany jest jeden z operatorÛw *> = <*, nastÍpnie data i opcjonalnie godzina np. '> 11/01/2017 17:05'
  * Po dacie zakoÒczenia po≥πczenia, wymagany jest jeden z operatorÛw *> = <*, nastÍpnie data i opcjonalnie godzina np. '> 11/01/2017 17:05'
  * Po rodzaju po≥πczenia, jeden z rodzajÛw po≥πczenia wybierany za pomocπ cyfry 1-6
  * Po op≥acie za po≥πczenie, naleøy podaÊ jeden z operatorow *> = <* oraz kwotÍ np. '> 25'
- Moøliwe jest zapisanie przefiltrowanie danych do pliku, domyúlnie jest to plik 'filtered.txt' w folderze g≥Ûwnym 'CDR_Analyzer', moøna to zmieniÊ w pliku 'SaveData.cs' w linijce 'System.IO.File.WriteAllLines("..\\..\\filtered.txt", lines);'
- Dzia≥anie na przefiltrowanych danych jest moøliwe, bez wzglÍdu na ürÛd≥o danych odbywa siÍ ono na listach, aby nie by≥o moøliwoúci nadpisania pierwotnych danych w bazie
- W kaødym momencie moøliwe jest ponowne wczytanie danych z pliku

## Autorzy
Adrian Sondej - [Ardoos](https://github.com/Ardoos)

## Licencja
Projekt dostÍpny jest na licencji MIT, sprawdü [LICENSE.MD](LICENSE.md), aby dowiedzieÊ siÍ wszystkich szczegÛ≥Ûw

## èrÛd≥o danych CDR
- Program korzysta z [pliku z danymi CDR](CDR_Analyzer/cdr_small.txt) stworzonym przez [generator](https://github.com/mayconbordin/cdr-gen)