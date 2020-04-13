--CREATE_USER D
CREATE OR REPLACE FUNCTION create_user(ime VARCHAR(150), priimek VARCHAR(150), gmail VARCHAR(250), geslo VARCHAR(255), tel VARCHAR(16)) 
RETURNS INTEGER AS
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
LANGUAGE 'plpgsql'


--CREATE_PLACNIK D
CREATE OR REPLACE FUNCTION create_placnik(ime VARCHAR(150), priimek VARCHAR(150), gmail VARCHAR(250), geslo VARCHAR(255), tel VARCHAR(16)) 
RETURNS INTEGER AS
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
LANGUAGE 'plpgsql'

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