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

6ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
1ms surusiuoti faila "1000studentu.txt"
21ms surusiuoti faila "10000studentu.txt"
228ms surusiuoti faila "100000studentu.txt"
329ms surusiuoti 5 failus

# v0.5 efektyvumas:

LIST rusiavimas:
6ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
1ms surusiuoti faila "1000studentu.txt"
21ms surusiuoti faila "10000studentu.txt"
228ms surusiuoti faila "100000studentu.txt"
329ms surusiuoti 5 failus

QUEUE rusiavimas:
0ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
1ms surusiuoti faila "1000studentu.txt"
20ms surusiuoti faila "10000studentu.txt"
251ms surusiuoti faila "100000studentu.txt"
310ms surusiuoti 5 failus

LINKEDLIST rusiavimas:
0ms surusiuoti faila "10studentu.txt"
0ms surusiuoti faila "100studentu.txt"
4ms surusiuoti faila "1000studentu.txt"
29ms surusiuoti faila "10000studentu.txt"
260ms surusiuoti faila "100000studentu.txt"
360ms surusiuoti 5 failus

# v1.0 atminties naudojimas skirtingom strategijom ir laikas

# 1 strategija:
LIST rusiavimas:
5ms surusiuoti faila "10studentu.txt" Memory used: 10Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 10Mb
1ms surusiuoti faila "1000studentu.txt" Memory used: 11Mb
20ms surusiuoti faila "10000studentu.txt" Memory used: 17Mb
225ms surusiuoti faila "100000studentu.txt" Memory used: 36Mb
316ms surusiuoti 5 failus

QUEUE rusiavimas:
0ms surusiuoti faila "10studentu.txt" Memory used: 45Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 45Mb
1ms surusiuoti faila "1000studentu.txt" Memory used: 45Mb
20ms surusiuoti faila "10000studentu.txt" Memory used: 46Mb
238ms surusiuoti faila "100000studentu.txt" Memory used: 37Mb
297ms surusiuoti 5 failus

LINKEDLIST rusiavimas:
0ms surusiuoti faila "10studentu.txt" Memory used: 43Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 43Mb
1ms surusiuoti faila "1000studentu.txt" Memory used: 43Mb
28ms surusiuoti faila "10000studentu.txt" Memory used: 42Mb
270ms surusiuoti faila "100000studentu.txt" Memory used: 57Mb
326ms surusiuoti 5 failus

# 2 strategija:
LIST rusiavimas:
6ms surusiuoti faila "10studentu.txt" Memory used: 10Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 10Mb
2ms surusiuoti faila "1000studentu.txt" Memory used: 11Mb
97ms surusiuoti faila "10000studentu.txt" Memory used: 17Mb
14175ms surusiuoti faila "100000studentu.txt" Memory used: 36Mb
14105ms surusiuoti 5 failus

QUEUE rusiavimas:
0ms surusiuoti faila "10studentu.txt" Memory used: 44Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 44Mb
1ms surusiuoti faila "1000studentu.txt" Memory used: 44Mb
25ms surusiuoti faila "10000studentu.txt" Memory used: 45Mb
257ms surusiuoti faila "100000studentu.txt" Memory used: 39Mb
378ms surusiuoti 5 failus
LINKEDLIST rusiavimas:

0ms surusiuoti faila "10studentu.txt" Memory used: 51Mb
0ms surusiuoti faila "100studentu.txt" Memory used: 51Mb
1ms surusiuoti faila "1000studentu.txt" Memory used: 52Mb
24ms surusiuoti faila "10000studentu.txt" Memory used: 54Mb
267ms surusiuoti faila "100000studentu.txt" Memory used: 41Mb
341ms surusiuoti 5 failus

Matome, kad lasinant is LIST irasus tai kainuoja labai daug laiko ir tikrai neverta to daryti del keliu laimetu Mb atminties.

# Paleidimo instrukcija:
Parsisiusti projekto repozitorija ir github.com;
Su betkuria IDE importuoti koda;
Kompiliuoti projekta;

# How to use:
Sukompiliavus projekta pasileisti IPA.exe.
*Patarimas leisti programa tiesiai is IDE, kadangi ivykdzius viena is meniu pasirinkimu programa baigia darba.
Pasirinkus komanda siuloma is meniu ivedus jos skaiciu;
Ivykdzius viena komanda programa baigia darba.