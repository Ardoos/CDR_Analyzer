# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## CDR-Analyzer [Unreleased]

## 0.0.5 - 2018-05-29
### Zmodyfikowane
- Wyświetlanie błądu w przypadku nieprawidłowych danych/złej liczby danych w linii
- Wyświetlanie błędu w przypadku pustego pliku/braku danych
- Obsługa i sprawdzanie danych numerycznych wykraczających poza zakres zmiennej typu Integer
- Obsługa nieoczekiwanych błędów z możliwością kontynuacji programu

## 0.0.4 - 2018-05-04
### Dodane
- Działanie programu w pętli, dopóki użytkownik sam go nie zamknie
- Dodanie głównego menu
- Możliwość resetowania danych z pliku
- Możliwość zastosowania wielu filtrów

### Zmodyfikowane
- Sposób wyświetlania wiadomości, teraz w jednej klasie MessageController
- Zarządzanie pamięcią po przefiltrowaniu danych (ograniczanie wielkości listy)


## 0.0.3 - 2018-05-02
### Dodane
- Analiza danych CDR: filtrowanie na podstawie numerów telefonu, daty połączeń, rodzaju i ceny
- Obsługa zapytań z wiersza poleceń

## 0.0.2 - 2018-05-01
### Dodane
- Plik changelog.md
- Plik readme.md
- Plik license


## 0.0.1 - 2018-05-01
### Dodane
- Przetwarzanie danych CDR z pliku na listę
- Zapytania do stworzonej listy (modyfikowalne w kodzie)
- Podstawowe wyświetlanie danych

