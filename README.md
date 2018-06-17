# Process Tracing

Aplikacja webowa korzystająca z API Trello w celu graficznego przedstawienia pracy na tablicach aplikacji [Trello](https://trello.com), pozwalająca na jej znalizę.

Konfigutacja
-----------

W pliku `TrelloProvider.cs` należy dodać [key](https://trello.com/app-key) oraz [token](https://trello.com/1/authorize?expiration=never&scope=read,write,account&response_type=token&name=Server%20Token&key=0a9c02bbe066e66428ef7d8689020999), które można pobrać dla swojego konta z podpiętych stron.

Rozwój
-----------
  1. Dodanie pobierania tokena użytkownika do aplikacji przy logowaniu.
  2. Dodanie asynchronicznego wysyłania requestów do api.

Licencja
-----------
[LICENCJA](LICENSE)
