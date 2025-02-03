# Melbourne Coworking Spaces

“Melbourne Coworking Spaces” è un progetto MVC sviluppato in .NET 6 con Visual Studio,
che permette di recuperare i dati da un file JSON contenente una lista di spazi di coworking
situati a Melbourne. I dati vengono visualizzati in un'interfaccia web che offre funzionalità
di ricerca e filtro per facilitare la consultazione. Inoltre, è stato implementato un servizio
SOAP (creato con l'utilizzo della libreria SoapCore) che consente di visualizzare la lista dei
coworking spaces in formato XML.
Per garantire una distribuzione e un'esecuzione consistenti su qualsiasi ambiente, il 
progetto è stato successivamente containerizzato utilizzando Docker.

## 🚀 Funzionalità
- 🔎 Visualizzazione dei Coworking Spaces: Mostra una lista di tutti i coworking spaces di Melbourne e il loro posizionamento nella mappa (OSM).
- 📑 Filtraggio e Ricerca: Permette di filtrare i coworking spaces in base al nome o ricercarli rapidamente attraverso una barra di ricerca.
- 📡 Servizio SOAP: Fornisce un endpoint SOAP (`/Service.wsdl`) per ottenere la lista dei coworking spaces in formato XML.

## 🛠️ Tecnologie utilizzate
- **.NET 6:** Framework di sviluppo per l'applicazione web.
- **Servizio SOAP (libreria SoapCore):** Protocollo per lo scambio di messaggi XML tra applicazioni.
- **JSON NewtonSoft:** Libreria per la manipolazione dei dati JSON.
- **SoapCore**: Libreria per la creazione di servizi SOAP in .NET.
- **Docker:** Containerizzazione dell'applicazione per una distribuzione più efficiente.

## 💻 Struttura del progetto
- **Models:** Contiene le classi che rappresentano i dati dei coworking spaces.
- **Views:** Contiene i file Razor (.cshtml) dell'interfaccia web.
- **Controllers:** Contiene i controller per gestire le richieste HTTP, elabora i dati tramite i modelli e restituisce le viste appropriate.
- **Services:** Contiene i servizi per il recupero e la gestione dei dati
- **ServizioSOAP:** Implementa il servizio SOAP con i metodi **GetAllData** (mostra tutti i coworking spaces), **GetFilteredData** (mostra solamente i coworking spaces che corrispondono a quello cercato), **GetDataLimit** (mostra un numero limitato di coworking spaces).
- **wwwroot:** Contiene file statici come CSS, JavaScript e il file JSON dei coworking spaces.
- **appsettings.json:** Configurazioni del progetto.
- **Dockerfile:** File per la creazione dell'immagine Docker.

## ✨ Stile e Layout
- layout generati automaticamente al momento della creazione del progetto. ???
- **Bootstrap 5:** Framework CSS per la progettazione dell'interfaccia web.
- **Leaflet.js:** Libreria JavaScript per la visualizzazione delle mappe (?).
- **OpenStreetMap:** Mappa interattiva per visualizzare i coworking spaces.

## 📦 Installazione
1. Clonare il repository:
   ```bash
   git clone https://github.com/Pipperix/MelbourneCoworkingSpaces
2. Aprire il Progetto in Visual Studio:
- Apri Visual Studio.
- Seleziona "Apri un progetto o una soluzione".
- Naviga fino alla cartella del progetto e seleziona il file .sln.
3. Eseguire l'Applicazione:
Premi F5 o clicca su "Esegui" per avviare l'applicazione in modalità debug.
4. Accesso al Servizio SOAP:
Per accedere al servizio SOAP, vai a http://localhost:5070/Service.wsdl.

## 🐳 Docker
1. Creare l'immagine Docker:
   ```bash
   docker build -t melbournecoworkingspaces_web .
2. Eseguire il container Docker:
   ```bash
   docker run -d --name melbournecoworkingspaces_web -p 8080:80 melbournecoworkingspaces_web

**melbournecoworkingspaces_web** è il nome dell'immagine Docker, dunque è personalizzabile.

## 🤝 Contributori
- **Forconi Leonardo** (mat. )
- **Marsili Davide** (mat. 123284)
- **Medei Chiara** (mat. 123285)
