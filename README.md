# Dokumentacja projektu – Biblioteka (PZPP)

## 1. Opis projektu

### Co aplikacja robi
Aplikacja **Biblioteka** to desktopowy system do zarządzania katalogiem bibliotecznym.
Umożliwia bibliotekarzowi przeglądanie, dodawanie, edytowanie i usuwanie książek,
autorów, gatunków literackich oraz filii biblioteki. Dane przechowywane są w bazie in-memory,
a przy każdym uruchomieniu aplikacja automatycznie generuje przykładowe dane testowe,
w tym stany magazynowe dla każdej filii.

### Dla kogo jest
System przeznaczony jest dla pracowników biblioteki (bibliotekarzy), którzy
na co dzień zarządzają katalogiem zbiorów bibliotecznych oraz dostępnością egzemplarzy
w poszczególnych filiach.

---

## 2. Wymagania funkcjonalne

| ID | Wymaganie |
|----|-----------|
| WF-01 | Użytkownik może przeglądać listę wszystkich książek w katalogu |
| WF-02 | Użytkownik może dodawać nowe książki z podaniem tytułu, autora, gatunku i ilości na stanie |
| WF-03 | Użytkownik może edytować dane istniejącej książki |
| WF-04 | Użytkownik może usuwać książki z katalogu po potwierdzeniu operacji |
| WF-05 | Użytkownik może zarządzać listą autorów (dodawanie, edycja, usuwanie) |
| WF-06 | Użytkownik może zarządzać gatunkami literackimi (dodawanie, edycja, usuwanie) |
| WF-07 | Użytkownik może zarządzać filiami biblioteki (dodawanie, edycja, usuwanie) |
| WF-08 | Użytkownik może filtrować listę książek według tytułu, autora lub gatunku |
| WF-09 | Użytkownik może wybrać filię i zobaczyć dostępność książek w tej filii |
| WF-10 | Użytkownik może sortować listę książek alfabetycznie według tytułu |
| WF-11 | Użytkownik może zobaczyć szczegóły wybranej książki w osobnym oknie |
| WF-12 | Aplikacja wyświetla łączną liczbę egzemplarzy w katalogu z uwzględnieniem wybranej filii |
| WF-13 | Przy uruchomieniu aplikacja automatycznie wypełnia bazę przykładowymi danymi |

---

## 3. Wymagania niefunkcjonalne

| ID | Wymaganie |
|----|-----------|
| WNF-01 | **Wydajność** – aplikacja powinna uruchamiać się w czasie poniżej 3 sekund na standardowym komputerze biurowym |
| WNF-02 | **Niezawodność** – operacje zapisu i usuwania danych są natychmiastowo odzwierciedlane w widoku listy |
| WNF-03 | **Użyteczność** – interfejs jest intuicyjny i nie wymaga szkolenia; każda sekcja dostępna z poziomu głównego okna |
| WNF-04 | **Przenośność** – aplikacja działa na systemie Windows z zainstalowanym .NET 8 bez dodatkowej konfiguracji zewnętrznej bazy danych |
| WNF-05 | **Spójność danych** – relacje między encjami zdefiniowane w EF Core zapobiegają niespójnościom przy usuwaniu rekordów |
| WNF-06 | **Testowalność** – użycie bazy in-memory upraszcza testowanie logiki bez konieczności konfiguracji zewnętrznego serwera SQL |
| WNF-07 | **Walidacja** – aplikacja uniemożliwia zapis niepoprawnych danych i informuje użytkownika o błędach przez komunikaty |

---

## 4. Role użytkowników

| Rola | Opis | Uprawnienia |
|------|------|-------------|
| **Bibliotekarz** | Pracownik obsługujący system na stanowisku komputerowym | Pełny dostęp – przeglądanie, dodawanie, edytowanie i usuwanie książek, autorów, gatunków oraz filii |

> Aplikacja nie posiada systemu logowania – zakłada się, że dostęp fizyczny do stanowiska
> jest wystarczającym zabezpieczeniem. Rozbudowa o uwierzytelnianie może być przedmiotem
> przyszłych wersji.

---

## 5. Przypadki użycia

### UC-01 – Dodanie nowej książki

**Aktor:** Bibliotekarz  
**Warunek wstępny:** Aplikacja jest uruchomiona; w systemie istnieje co najmniej jeden autor i jeden gatunek  
**Przebieg:**
1. Użytkownik klika przycisk „Zarządzaj Książkami" w oknie głównym
2. W oknie książek klika „Dodaj Książkę"
3. Wypełnia formularz: tytuł, wybiera autora z listy, gatunek, podaje ilość na stanie
4. Klika „Zapisz"
5. Nowa książka pojawia się na liście

**Warunek końcowy:** Książka jest zapisana w bazie i widoczna na liście

---

### UC-02 – Edycja gatunku literackiego

**Aktor:** Bibliotekarz  
**Warunek wstępny:** W systemie istnieje co najmniej jeden gatunek  
**Przebieg:**
1. Użytkownik klika „Zarządzaj Gatunkami" w oknie głównym
2. Zaznacza gatunek na liście
3. Klika „Edytuj"
4. Zmienia nazwę gatunku w oknie edycji
5. Klika „Zapisz"

**Warunek końcowy:** Zmieniona nazwa gatunku jest widoczna na liście

---

### UC-03 – Usunięcie autora

**Aktor:** Bibliotekarz  
**Warunek wstępny:** W systemie istnieje co najmniej jeden autor  
**Przebieg:**
1. Użytkownik klika „Zarządzaj Autorami" w oknie głównym
2. Zaznacza autora na liście
3. Klika „Usuń"
4. Aplikacja wyświetla okno dialogowe z prośbą o potwierdzenie
5. Użytkownik potwierdza – autor zostaje usunięty z bazy

**Warunek końcowy:** Autor znika z listy

---

### UC-04 – Przeglądanie katalogu książek z filtrowaniem po filii

**Aktor:** Bibliotekarz  
**Warunek wstępny:** Aplikacja jest uruchomiona; w systemie istnieje co najmniej jedna filia  
**Przebieg:**
1. Użytkownik klika „Zarządzaj Książkami"
2. Na ekranie wyświetla się lista książek z tytułem, gatunkiem, ilością na stanie oraz autorem
3. Użytkownik wybiera filię z listy rozwijanej
4. Lista odświeża się i pokazuje dostępność książek w wybranej filii
5. Na pasku stanu wyświetla się łączna liczba egzemplarzy i nazwa wybranej filii

**Warunek końcowy:** Użytkownik widzi aktualny katalog zbiorów z dostępnością w wybranej filii

---

### UC-05 – Wyszukiwanie książki

**Aktor:** Bibliotekarz  
**Warunek wstępny:** Aplikacja jest uruchomiona; katalog zawiera co najmniej jedną książkę  
**Przebieg:**
1. Użytkownik otwiera okno książek
2. Wpisuje frazę w pole wyszukiwania
3. Lista automatycznie filtruje się po tytule, autorze lub gatunku

**Warunek końcowy:** Na liście wyświetlają się tylko książki pasujące do wpisanej frazy

---

### UC-06 – Zarządzanie filiami

**Aktor:** Bibliotekarz  
**Warunek wstępny:** Aplikacja jest uruchomiona  
**Przebieg:**
1. Użytkownik klika „Zarządzaj Filiami" w oknie głównym
2. Widzi listę filii z nazwą i lokalizacją
3. Może dodać nową filię, edytować istniejącą lub usunąć po potwierdzeniu

**Warunek końcowy:** Lista filii odzwierciedla wprowadzone zmiany

---

## 6. Model danych

### Encje

#### `Książka`
| Pole | Typ | Opis |
|------|-----|------|
| ISBN | int (PK) | Unikalny identyfikator książki |
| Tytuł | string | Tytuł książki |
| IloscNaStanie | int | Łączna liczba egzemplarzy (ogólna) |
| GatunekID | int (FK) | Klucz obcy do gatunku |
| AutorID | int? (FK) | Klucz obcy do autora |

#### `Autor`
| Pole | Typ | Opis |
|------|-----|------|
| ID | int (PK) | Unikalny identyfikator autora |
| Imię | string | Imię autora |
| Nazwisko | string | Nazwisko autora |

#### `GatunekKsiążki`
| Pole | Typ | Opis |
|------|-----|------|
| ID | int (PK) | Unikalny identyfikator gatunku |
| Nazwa | string | Nazwa gatunku (np. Horror, Fantasy) |

#### `Filia`
| Pole | Typ | Opis |
|------|-----|------|
| ID | int (PK) | Unikalny identyfikator filii |
| Nazwa | string | Nazwa filii (np. Filia Główna) |
| Lokalizacja | string | Adres filii |

#### `StanMagazynowy`
| Pole | Typ | Opis |
|------|-----|------|
| ID | int (PK) | Unikalny identyfikator rekordu |
| KsiążkaISBN | int (FK) | Klucz obcy do książki |
| FiliaID | int (FK) | Klucz obcy do filii |
| IloscNaStanie | int | Liczba egzemplarzy dostępnych w danej filii |

### Relacje

- `GatunekKsiążki` **1 → N** `Książka` (jeden gatunek może mieć wiele książek)
- `Autor` **1 → N** `Książka` (jeden autor może mieć wiele książek)
- `Książka` **1 → N** `StanMagazynowy` (jedna książka może mieć stan w wielu filiach)
- `Filia` **1 → N** `StanMagazynowy` (jedna filia może przechowywać wiele książek)

---

## 7. Architektura systemu

Aplikacja zbudowana jest zgodnie z wzorcem **MVVM (Model–View–ViewModel)** w technologii WPF (.NET 8).

```mermaid
flowchart TD
    UI["Warstwa UI\nMainWindow / GatunekWindow / KsiążkaWindow\nAutorWindow / FiliaWindow"]
    VM["Warstwa ViewModel\nMainViewModel / GatunekViewModel\nKsiążkaViewModel / AutorViewModel / FiliaViewModel"]
    DAL["Warstwa danych\nBiblioteka DbContext\nDbSet Książka / Autor / GatunekKsiążki\nFilia / StanMagazynowy"]
    MEM["Baza danych InMemory\nEF Core InMemory"]

    UI -->|"Data Binding"| VM
    VM -->|"EF Core DbContext"| DAL
    DAL -->|"UseInMemoryDatabase"| MEM
```

Kontener DI (`Microsoft.Extensions.Hosting`) zarządza cyklem życia wszystkich okien
i ViewModeli, umożliwiając wstrzykiwanie zależności (m.in. `DbContext`) do konstruktorów.

---

## 8. Technologie

| Technologia | Wersja | Uzasadnienie |
|-------------|--------|--------------|
| **C# / .NET 8** | 8.0 | Nowoczesna, wydajna platforma dla aplikacji desktopowych Windows |
| **WPF (Windows Presentation Foundation)** | .NET 8 | Dojrzały framework UI dla aplikacji Windows z pełną obsługą wzorca MVVM i data bindingu |
| **Entity Framework Core** | 9.0.14 | ORM umożliwiający pracę z bazą danych bez pisania zapytań SQL; łatwa migracja na SQL Server w przyszłości |
| **EF Core InMemory** | 9.0.14 | Baza danych w pamięci operacyjnej – idealna do prototypowania, nie wymaga instalacji serwera |
| **Microsoft.Extensions.Hosting** | 9.0.14 | Kontener dependency injection zgodny ze standardem .NET – umożliwia wstrzykiwanie DbContext i ViewModeli |
| **Bogus** | 35.6.5 | Biblioteka do generowania realistycznych danych testowych (imiona, tytuły) z obsługą języka polskiego |
