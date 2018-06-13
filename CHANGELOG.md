# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## CDR-Analyzer

## 1.1.5 - 2018-06-14
### Dodane
 - Odnośniki do poszczególnych commitów w pliku CHANGELOG

## [1.1.4 - 2018-06-14](https://github.com/Ardoos/CDR_Analyzer/commit/c76f064ca5cae9e298ae50c8c3399df5d89b3017)
### Dodane
 - Komentarze do wygenerowania dokumentacji

### Poprawione
- Błędy związane z filtrowaniem wiadomości na listach
- Błąd filtrowania daty na bazie danych

## [1.1.3 - 2018-06-14](https://github.com/Ardoos/CDR_Analyzer/commit/7bfb7dd9ffa8d35ac6a4b2122d502550a36d3919)
### Poprawione
 - Formatowanie w pliku README.md

## [1.1.2 - 2018-06-14](https://github.com/Ardoos/CDR_Analyzer/commit/e2388f512190e90c3f58ae90fba44a48f8b7eab2)
### Dodane
 - Instrukcje w pliku README.md
 - Zoptymalizowane zapytania w przypadku błędnych danych (natychmiastowa odpowiedź, jeśli podane dane nie pasują do przyjętego wzorca)

## [1.1.1 - 2018-06-13](https://github.com/Ardoos/CDR_Analyzer/commit/40c33a0317bb797140069770a74702673a8edbbe)
### Dodane
- Możliwość przełączenia listy/bazy danych w czasie pracy programu
- Wyświetlanie komunikatów w przypadku wybraniu nieprawidłowych opcji
- Dodawanie rekordów do bazy danych co określoną liczbę rekordów, nie pojedyczno
- Komunikaty o oczekiwaniu, potrzebne szczególnie w przypadku przetwarzania dużych ilości danych

### Poprawione
- Spójność komunikatów przekazywanych użytkownikowi

## [1.1.0 - 2018-06-08](https://github.com/Ardoos/CDR_Analyzer/commit/bf80d30845541b1b81e876b7faef673d558493de)
### Dodane
- Wyświetlanie czasu, który potrzebny był na wykonanie zapytania do listy/bazy danych
- Możliwość wyboru gdzie przechowywane będą dane (lista/baza danych)
- Zapisywanie wyników zapytań do pliku
- Możliwość wykonywania dalszych operacji na otrzymanym wyniku zapytania

### Zmodyfikowane
- Uproszczona obsługa interfejsu (wybieranie numerów zamiast wpisywania pełnych wyrazów zapytań)

### Poprawione
- Nieprawidłowe dane wprowadzone przez użytkownika nie powodują już zakończenia pracy aplikacji

## [1.0.0 - 2018-06-03](https://github.com/Ardoos/CDR_Analyzer/commit/09a05af20799503763604b62bb72b80d9aa09aed)
### Dodane
- Dane przechowywane są na MongoDB - baza NoSQL
- Klasa DB.cs odpowiedzialna za komunikację z bazą danych
- Klasa CallRecord.cs, następca DataRow, klasa rekordu dopasowana do potrzeb MognoDB

### Zmodyfikowane
- Na starcie programu dane nie są wczytywane, chyba że baza jest pusta. Licznik ilości wczytanych rekordów podczas wczytywania
- Zapytania zostały zmodyfikowane tak, aby poprawnie działały z bazą danych
- Wyświetlanie danych strona po stronie
- Statyczny MessageController

## [0.0.5 - 2018-05-29](https://github.com/Ardoos/CDR_Analyzer/commit/37abbad23a43f8232cd2ae43afa7e54b6d450810)
### Zmodyfikowane
- Wyświetlanie błądu w przypadku nieprawidłowych danych/złej liczby danych w linii
- Wyświetlanie błędu w przypadku pustego pliku/braku danych
- Obsługa i sprawdzanie danych numerycznych wykraczających poza zakres zmiennej typu Integer
- Obsługa nieoczekiwanych błędów z możliwością kontynuacji programu

## [0.0.4 - 2018-05-04](https://github.com/Ardoos/CDR_Analyzer/commit/ace7e2bd3c7f90aeea6a1d8e55efb395fd609783)
### Dodane
- Działanie programu w pętli, dopóki użytkownik sam go nie zamknie
- Dodanie głównego menu
- Możliwość resetowania danych z pliku
- Możliwość zastosowania wielu filtrów

### Zmodyfikowane
- Sposób wyświetlania wiadomości, teraz w jednej klasie MessageController
- Zarządzanie pamięcią po przefiltrowaniu danych (ograniczanie wielkości listy)


## [0.0.3 - 2018-05-02](https://github.com/Ardoos/CDR_Analyzer/commit/029609a96d4f172e5c4eeb9ca4b7dc237a97bd79)
### Dodane
- Analiza danych CDR: filtrowanie na podstawie numerów telefonu, daty połączeń, rodzaju i ceny
- Obsługa zapytań z wiersza poleceń

## [0.0.2 - 2018-05-01](https://github.com/Ardoos/CDR_Analyzer/commit/f3238b770d2f041093f0a3a90122584ba44cee49)
### Dodane
- Plik changelog.md
- Plik readme.md
- Plik license


## [0.0.1 - 2018-05-01](https://github.com/Ardoos/CDR_Analyzer/commit/210dcc2154ca383c15a7725f79e18e669d06d28b)
### Dodane
- Przetwarzanie danych CDR z pliku na listę
- Zapytania do stworzonej listy (modyfikowalne w kodzie)
- Podstawowe wyświetlanie danych

