IPA 3-4

Integruotu programavimo aplinku 3-4 laboratorinis darbas.

# v0.1

Prideta:
- Studento informacijos nuskaitymas;
- Pasirinkimas automatiniam rezultatu generavimui;
- Vidurkio arba medianos skaiciavimas;
- Informacijos isvedimas i konsoles langa;
- Papildomas .cs failas, demonstruojantis List<> pakeitima Array tipu rezultatu surinkimui;

# v0.2

Prideta:
- Duomenu nuskaitymas is failo;
- Logikos blokai perkelti i nuosavus metodus;
- Perdarytas konsoles isvedimas;
- Isvedami duomenys isrikiuojami pagal varda didejimo tvarka;

# v0.3

Prideta:
- Iskelta Student klase i nauja faila;
- Iskelti meniu pasirinkimai i atskirus metodus;
- Pritaikytas exceptions valdymas;
- Klaidos taisymas;

# v0.4 efektyvumas

Prideta:
- Pridetas meniu pasirinkimas "efektyvumas";
- Cia sugeneruojami 5 failai su skirtingais sarasu ilgiais;
- Pridetas studentu grupavimas pagal galutini pazymi i failus;

efektyvumas ( LIST: rusiavimas + failu isvedimas):

554ms sugeneruoti 5 failus.
20ms surusiuoti faila "10studentu.txt"
34ms surusiuoti faila "100studentu.txt"
7ms surusiuoti faila "1000studentu.txt"
94ms surusiuoti faila "10000studentu.txt"
762ms surusiuoti faila "100000studentu.txt"
1209ms surusiuoti 5 failus

# v0.5 efektyvumas:

LIST rusiavimas.
23ms surusiuoti faila "10studentu.txt"
3ms surusiuoti faila "100studentu.txt"
11ms surusiuoti faila "1000studentu.txt"
83ms surusiuoti faila "10000studentu.txt"
597ms surusiuoti faila "100000studentu.txt"
759ms surusiuoti visus 5 failus

QUEUE rusiavimas.
1ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
6ms surusiuoti faila "1000studentu.txt"
68ms surusiuoti faila "10000studentu.txt"
595ms surusiuoti faila "100000studentu.txt"
720ms surusiuoti visus 5 failus

LINKEDLIST rusiavimas.
1ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
6ms surusiuoti faila "1000studentu.txt"
61ms surusiuoti faila "10000studentu.txt"
592ms surusiuoti faila "100000studentu.txt"
757ms surusiuoti visus 5 failus

QUEUE ir LINKEDLIST trunka beveik tiek pat. 
Greiciausiai 5 failus rusiuoja QUEUE.
LIST mazu failu rusiavimas uztrunka zymiai ilgiau nei kiti.
10000 studentu faila, greiciausiai nuskaito LINKEDLIST, lenkdamas kitus vos pora ms.

# v1.0


