# GuitarAssistant

Narzędzie konsolowe (CLI) dla gitarzystów, które wypisuje dźwięki wybranej skali modalnej
zbudowanej od podanego dźwięku.

## Wymagania

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Budowanie i uruchamianie

Wszystkie polecenia uruchamiaj z katalogu `src`:

```sh
cd src
dotnet build
dotnet run --project GuitarAssistant.Cli -- -scale=aeolian -root=A
```

Wynik:

```
Notes for scale Aeolian A:
A H C D E F G A
```

## Testy

Testy jednostkowe (xUnit) znajdują się w projekcie `GuitarAssistant.Tests`:

```sh
cd src
dotnet test
```

Testy obejmują:
- `NotesCalculatorTests` – wzory interwałów wszystkich skal, dźwięki skal w różnych tonacjach, zawijanie w obrębie oktawy, odrzucanie niepoprawnych wartości,
- `CommandParserTests` – poprawne argumenty, kolejność i wielkość liter, błędne wartości, brakujące, powtórzone i nieznane argumenty,
- `PrinterTests` – wypisywanie wartości enumów przez `ILogger`.

## Użycie

```
GuitarAssistant.Cli.exe -scale=<skala> -root=<dźwięk>
```

| Argument | Opis                                                     | Przykład        |
|----------|----------------------------------------------------------|-----------------|
| `-scale` | Nazwa skali modalnej                                     | `-scale=dorian` |
| `-root`  | Dźwięk podstawowy (tonika), od którego budowana jest skala | `-root=Fis`     |

- Oba argumenty są wymagane, a ich kolejność nie ma znaczenia.
- Wielkość liter nie ma znaczenia (`-scale=Dorian` = `-scale=dorian`).
- Wywołanie bez argumentów albo z `-h` / `-help` / `--help` wyświetla pomoc i listę dozwolonych wartości.
- Przy błędnych argumentach program wypisuje komunikat błędu i pomoc, a kończy się kodem wyjścia `1`.

### Dostępne skale

| Skala        | Wzór interwałów (półtony) |
|--------------|---------------------------|
| `Ionian`     | 2 2 1 2 2 2 1             |
| `Dorian`     | 2 1 2 2 2 1 2             |
| `Phrygian`   | 1 2 2 2 1 2 2             |
| `Lydian`     | 2 2 2 1 2 2 1             |
| `Mixolydian` | 2 2 1 2 2 1 2             |
| `Aeolian`    | 2 1 2 2 1 2 2             |
| `Locrian`    | 1 2 2 1 2 2 2             |

### Dostępne dźwięki

Dźwięki używają notacji niemieckiej/polskiej (krzyżyki z końcówką `-is`, `B` = B♭, `H` = B):

`C`, `Cis`, `D`, `Dis`, `E`, `F`, `Fis`, `G`, `Gis`, `A`, `B`, `H`

## Struktura projektu

```
src/
├── GuitarAssistant.sln
├── GuitarAssistant.Cli/           # aplikacja konsolowa
│   ├── Program.cs                 # punkt wejścia, obsługa błędów, pomoc
│   ├── CommandParser.cs           # parsowanie i walidacja argumentów
│   ├── Parameters.cs              # sparsowane parametry (skala + dźwięk)
│   └── Constants.cs               # tekst pomocy
├── GuitarAssistant.ScalesEngine/  # logika muzyczna
│   ├── Engine/NotesCalculator.cs  # wyznaczanie dźwięków skali
│   └── Enums/                     # Note, Scale
├── GuitarAssistant.Common/        # elementy wspólne
│   ├── Interfaces/ILogger.cs
│   └── Services/                  # ConsoleLogger, Printer
└── GuitarAssistant.Tests/         # testy jednostkowe (xUnit)
```

## Jak to działa

Wszystkie skale modalne są przesunięciami (rotacjami) wzoru skali jońskiej `2 2 1 2 2 2 1`.
Wartość liczbowa w enumie `Scale` (0–6) jest jednocześnie przesunięciem tego wzoru, np. `Dorian = 1`
oznacza wzór zaczynający się od drugiego interwału: `2 1 2 2 2 1 2`.

`NotesCalculator.GetNotes` zaczyna od podanego dźwięku i kolejno dodaje interwały ze wzoru,
zawijając wynik w obrębie oktawy (12 półtonów). Na końcu listy pojawia się dźwięk początkowy
o oktawę wyżej.

## Licencja

Zobacz plik [LICENSE](LICENSE).
