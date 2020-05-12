--CREATE_USER D
CREATE OR REPLACE FUNCTION create_user(ime VARCHAR(150), priimek VARCHAR(150), gmail VARCHAR(250), geslo VARCHAR(255), tel VARCHAR(16)) 
RETURNS BOOLEAN AS
$$
    BEGIN
       IF((SELECT COUNT(u.id) FROM users u WHERE u.email = gmail)=0) 
       THEN
            INSERT INTO users (ime, priimek, email, geslo, tel) VALUES (ime, priimek, gmail, geslo, tel);
            RETURN TRUE;
        ELSE
            RETURN FALSE;
        END IF;
    END;
$$
LANGUAGE 'plpgsql';


--CREATE_PLACNIK D
CREATE OR REPLACE FUNCTION create_placnik(ime VARCHAR(150), priimek VARCHAR(150), gmail VARCHAR(250), geslo VARCHAR(255), tel VARCHAR(16)) 
RETURNS BOOLEAN AS
$$
    BEGIN
       IF((SELECT COUNT(p.id) FROM placniki p WHERE p.email = gmail)=0) 
       THEN
            INSERT INTO placniki (ime, priimek, email, geslo, tel) VALUES (ime, priimek, gmail, geslo, tel);
            RETURN TRUE;
        ELSE
            RETURN FALSE;
        END IF;
    END;
$$
LANGUAGE 'plpgsql';

--LOGIN (samo users)
CREATE OR REPLACE FUNCTION login(gmail VARCHAR(200), pass VARCHAR(255)) 
RETURNS BOOLEAN AS
$$
BEGIN
IF((SELECT COUNT(*) FROM users u WHERE u.email = gmail AND u.geslo = pass) = 0) 
    THEN
    RETURN FALSE;
    ELSE
    RETURN TRUE;
    END IF;
END;
$$
LANGUAGE 'plpgsql';


--LOGIN (samo users)
CREATE OR REPLACE FUNCTION loginp(gmail VARCHAR(200), pass VARCHAR(255)) 
RETURNS BOOLEAN AS
$$
BEGIN
IF((SELECT COUNT(*) FROM placniki p WHERE p.email = gmail AND p.geslo = pass) = 0) 
    THEN
    RETURN FALSE;
    ELSE
    RETURN TRUE;
    END IF;
END;
$$
LANGUAGE 'plpgsql';


--CREATE_KATEGORIJA
CREATE OR REPLACE FUNCTION kategorije_create(_kategorija VARCHAR(100), opis TEXT) 
RETURNS BOOLEAN AS
$$
    BEGIN
       IF((SELECT COUNT(p.id) FROM kategorije k WHERE k.kategorija = _kategorija)=0) 
       THEN
            INSERT INTO kategorije (kategorija, opis) VALUES (_kategorija, opis);
            RETURN TRUE;
        ELSE
            RETURN FALSE;
        END IF;
    END;
$$
LANGUAGE 'plpgsql';


--CREATE_STANJE
CREATE OR REPLACE FUNCTION stanja_create(_stanje VARCHAR(100), opis TEXT) 
RETURNS BOOLEAN AS
$$
    BEGIN
       IF((SELECT COUNT(p.id) FROM stanja s WHERE s.stanje = _stanje)=0) 
       THEN
            INSERT INTO kategorije (stanje, opis) VALUES (_stanje, opis);
            RETURN TRUE;
        ELSE
            RETURN FALSE;
        END IF;
    END;
$$
LANGUAGE 'plpgsql';


--IZPOSOJE CREATE
CREATE OR REPLACE FUNCTION izposoje_create(user_id INTEGER, oprema_id VARCHAR(150), placnik_id VARCHAR(250), stanje_id VARCHAR(255), datum_od TIMESTAMP, datum_do TIMESTAMP, opis TEXT) 
RETURNS BOOLEAN AS
$$
    BEGIN
        INSERT INTO izposoje (user_id, oprema_id, placnik_id, stanje_id, datum_od, datum_do, opis) VALUES (user_id, oprema_id, placnik_id, stanje_id, datum_od, datum_do, opis);
        RETURN TRUE;
    END;
$$
LANGUAGE 'plpgsql';


--OPREMA CREATE
CREATE OR REPLACE FUNCTION oprema_create(kategorija_id INTEGER, stanje_id VARCHAR(150), ime VARCHAR(250), opis TEXT) 
RETURNS BOOLEAN AS
$$
    BEGIN
        INSERT INTO placniki (kategorija_id, stanje_id, ime, opis) VALUES (kategorija_id, stanje_id, ime, opis);
        RETURN TRUE;
    END;
$$
LANGUAGE 'plpgsql';


--IZPOSOJE VIEW
CREATE OR REPLACE VIEW izposoje_view AS
SELECT i.id, u.ime || ' ' || u.priimek AS nadzornik, o.ime, p.ime || ' ' || p.priimek AS placnik, i.datum_od, i.datum_do, i.opis FROM users u 
INNER JOIN izposoje i ON u.id=i.user_id
INNER JOIN oprema o ON o.id=i.oprema_id
INNER JOIN placniki p ON p.id=i.placnik_id
INNER JOIN stanja s ON s.id=i.stanje_id;