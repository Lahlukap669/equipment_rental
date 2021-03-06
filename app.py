from flask import Flask, jsonify, request, send_file, send_from_directory
import json
import hashlib
from flask_cors import CORS
import os
import zipfile

app = Flask(__name__)

from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate

app.config['SQLALCHEMY_DATABASE_URI'] = "postgres://spdymswh:Rd2znszT-OTSAi7iH8g8eCeD8Ve0rRQo@kandula.db.elephantsql.com:5432/spdymswh"
db = SQLAlchemy(app)
migrate = Migrate(app, db)
CORS(app)


@app.route("/", methods=['GET', 'POST'])
def index():
    if (request.method == 'POST'):
        podatki_json = request.get_json()
        print(podatki_json["ime"])
        return jsonify({'ime': podatki_json["ime"]}), 201
    else:
        return jsonify({'u sent': "nothing"})

    

##REGISTER_USER
@app.route("/create_user", methods=['GET', 'POST'])
def create_user():
    if (request.method == 'POST'):
        podatki_json = request.get_json()
        # print(podatki_json)
        ##Deviding sent data
        ime = podatki_json["ime"]
        priimek = podatki_json["priimek"]
        email = podatki_json["email"]
        geslo = podatki_json["geslo"]
        tel = podatki_json["tel"]
        ##converting from string to byte
        geslo = bytes(geslo, 'utf-8')
        ##Hashing
        geslo_h = hashlib.sha256(geslo).hexdigest()

        ##interaction db
        try:
            ##Called function
            r=db.session.execute("""SELECT create_user('%s', '%s', '%s', '%s', '%s');""" % (ime, priimek, email, geslo_h, tel)).scalar()
            db.session.commit()
            ##Returned data to program
            return jsonify({'bool': r}), 201
        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'type': "cant use GET"})


##REGISTER_PLACNIK
@app.route("/create_placnik", methods=['GET', 'POST'])
def create_placnik():
    if (request.method == 'POST'):
        podatki_json = request.get_json()
        # print(podatki_json)
        ##Deviding sent data
        ime = podatki_json["ime"]
        priimek = podatki_json["priimek"]
        email = podatki_json["email"]
        geslo = podatki_json["geslo"]
        tel = podatki_json["tel"]
        ##converting from string to byte
        geslo = bytes(geslo, 'utf-8')
        ##Hashing
        geslo_h = hashlib.sha256(geslo).hexdigest()

        ##interaction db
        try:
            ##Called function
            r=db.session.execute("""SELECT create_placnik('%s', '%s', '%s', '%s', '%s');""" % (ime, priimek, email, geslo_h, tel)).scalar()
            db.session.commit()
            ##Returned data to program
            return jsonify({'bool': r}), 201
        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'type': "cant use GET"})


##LOGIN USER
@app.route("/login", methods=['GET', 'POST'])
def login():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "email": "luka1.lah@gmail.com",
        ##          "geslo": "Luka123"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        email = podatki_json["email"]
        geslo = podatki_json["geslo"]
        ##converting from string to byte
        geslo = bytes(geslo, 'utf-8')
        ##Hashing
        geslo_h = hashlib.sha256(geslo).hexdigest()
        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT login('%s', '%s');""" % (email, geslo_h)).scalar()
            db.session.commit()
            return jsonify({"bool": r}), 201

        except Exception as e:
            print(e)
            return jsonify({"bool": 0}), 404
    else:
        return jsonify({"u sent": "nothing"})

##LOGIN PLACNIK
@app.route("/loginp", methods=['GET', 'POST'])
def loginp():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "email": "luka1.lah@gmail.com",
        ##          "geslo": "Luka123"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        email = podatki_json["email"]
        geslo = podatki_json["geslo"]
        ##converting from string to byte
        geslo = bytes(geslo, 'utf-8')
        ##Hashing
        geslo_h = hashlib.sha256(geslo).hexdigest()
        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT login_p('%s', '%s');""" % (email, geslo_h)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##USER INFO
@app.route("/userinfo", methods=['GET', 'POST'])
def userinfo():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM users WHERE id=%s LIMIT 1;""" % (id)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "ime": "%s" % (r[1]), "priimek": "%s" % (r[2]), "email": "%s" % (r[3]), "tel": "%s"%(r[5])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


##PLACNIKI VSI
@app.route("/vplacniki", methods=['GET', 'POST'])
def vplacniki():
    if (request.method == 'POST'):
        ##interaction db
        r2 = {"placniki":[]}
        data = r2.get("placniki")
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM placniki;""").fetchall()
            db.session.commit()
            #r = str(r)[1:-1]
            #r = r.replace(" ", "")
            #r = r.replace("'", "")
            #r = r.split(",")
            print(r)
            for i in range(0, len(r)):
                r1 = {"id": int(r[i][0]), "ime": "%s" % (r[i][1]), "priimek": "%s" % (r[i][2]), "email": "%s" % (r[i][3]), "tel": "%s"%(r[i][5])}
                data.append(r1)
            print(data)
            return jsonify(data), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})

##KATEGORIJE CREATE
@app.route("/ckategorije", methods=['GET', 'POST'])
def ckategorije():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "kategorija": "vrtno orodje",
        ##          "opis": "orodje, ki se uporablja na vrtu ali za hišo"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        kategorija = podatki_json["kategorija"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT kategorije_create('%s', '%s');""" % (kategorija, opis)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##KATEGORIJE VSE
@app.route("/vkategorije", methods=['GET', 'POST'])
def vkategorije():
    if (request.method == 'POST'):
        ##interaction db
        r2 = {"kategorije":[]}
        data = r2.get("kategorije")
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM kategorije;""").fetchall()
            db.session.commit()
            #r = str(r)[1:-1]
            #r = r.replace(" ", "")
            #r = r.replace("'", "")
            #r = r.split(",")
            for i in range(0, len(r)):
                r1 = {"id": int(r[i][0]), "kategorija": "%s" % (r[i][1]), "opis": "%s" % (r[i][2])}
                data.append(r1)
            return jsonify(data), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})

##KATEGORIJE INFO
@app.route("/ikategorije", methods=['GET', 'POST'])
def ikategorije():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM kategorije WHERE id=%d LIMIT 1;""" % (id)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "kategorija": "%s" % (r[1]), "opis": "%s" % (r[2])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##STANJA CREATE
@app.route("/cstanja", methods=['GET', 'POST'])
def cstanja():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "stanje": "dobro",
        ##          "opis": "dobro ohranjeno z nekaj praskami"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        stanje = podatki_json["stanje"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT stanja_create('%s', '%s');""" % (stanje, opis)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##STANJA VSE
@app.route("/vstanja", methods=['GET', 'POST'])
def vstanja():
    if (request.method == 'POST'):
        ##interaction db
        r2 = {"stanja":[]}
        data = r2.get("stanja")
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM stanja;""").fetchall()
            db.session.commit()
            #r = str(r)[1:-1]
            #r = r.replace(" ", "")
            #r = r.replace("'", "")
            #r = r.split(",")
            for i in range(0, len(r)):
                r1 = {"id": int(r[i][0]), "stanje": "%s" % (r[i][1]), "opis": "%s" % (r[i][2])}
                data.append(r1)
            return jsonify(data), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


##OPREMA VSE
@app.route("/voprema", methods=['GET', 'POST'])
def voprema():
    if (request.method == 'POST'):
        ##interaction db
        r2 = {"oprema":[]}
        data = r2.get("oprema")
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM oprema;""").fetchall()
            db.session.commit()
            #r = str(r)[1:-1]
            #r = r.replace(" ", "")
            #r = r.replace("'", "")
            #r = r.split(",")
            for i in range(0, len(r)):
                r1 = {"id": int(r[i][0]), "kategorija_id":  int(r[i][1]), "stanje_id": int(r[i][2]), "ime": "%s" % (r[i][4]), "opis": "%s" % (r[i][3])}
                data.append(r1)
            return jsonify(data), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


##STANJA INFO
@app.route("/istanja", methods=['GET', 'POST'])
def istanja():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM stanja WHERE id=%d LIMIT 1;""" % (id)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "stanje": "%s" % (r[1]), "opis": "%s" % (r[2])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##POROCILA INFO
@app.route("/iporocila", methods=['GET', 'POST'])
def iporocila():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM porocila WHERE izposoja_id=%d LIMIT 1;""" % (id)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "stanje": "%s" % (r[1]), "opis": "%s" % (r[2])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})

##IZPOSOJE INFO
@app.route("/iizposoje", methods=['GET', 'POST'])
def iizposoje():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT id, user_id, oprema_id, placnik_id, stanje_id, TO_CHAR(datum_od,'YYYY-MM-DD HH24:MI:SS') as datum_od, TO_CHAR(datum_do,'YYYY-MM-DD HH24:MI:SS') as datum_do, opis FROM izposoje WHERE id=%d LIMIT 1;""" % (id)).first()
            db.session.commit()
            print(r)
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            
            r1 = {"id": int(r[0]), "nadzornik": int(r[1]), "oprema": int(r[2]), "placnik": int(r[3]), "stanje": int(r[4]), "datum_od": "%s" % (str(r[5][0:10] + " " + r[5][10:19])), "datum_do": "%s"%(str(r[6][0:10] + " " + r[6][10:19])), "opis": "%s"%(r[7])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})

##INFO OPREMA
@app.route("/ioprema", methods=['GET', 'POST'])
def ioprema():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM oprema WHERE id=%d LIMIT 1;""" % (id)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "kategorija_id":  int(r[1]), "stanje_id": int(r[2]), "ime": "%s" % (r[4]), "opis": "%s" % (r[3])}
            return jsonify(r1), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


##IZPOSOJE VSE
@app.route("/vizposoje", methods=['GET', 'POST'])
def vizposoje():
    if (request.method == 'POST'):
        r2 = {"izposoje":[]}
        data = r2.get("izposoje")
        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM izposoje_view;""").fetchall()
            db.session.commit()
            #r = str(r)[1:-1]
            #r = r.replace(" ", "")
            #r = r.replace("'", "")
            #r = r.split(",")
            for i in range(0, len(r)):
                r1 = {"id": int(r[i][0]), "nadzornik": "%s" % (r[i][1]), "oprema": "%s" % (r[i][2]), "placnik": "%s" % (r[i][3]), "stanje": "%s" % (r[i][4]), "datum_od": "%s" % (str(r[i][5])), "datum_do": "%s"%(str(r[i][6])), "opis": "%s"%(r[i][7])}
                data.append(r1)
            return jsonify(data), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##OPREMA CREATE
@app.route("/coprema", methods=['GET', 'POST'])
def coprema():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "kategorija_id": 1,
        ##          "stanje_id": 2,
        ##          "ime": "lopata",
        ##          "opis": "za grabljenje listja"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        kategorija_id = podatki_json["kategorija_id"]
        stanje_id = podatki_json["stanje_id"]
        ime = podatki_json["ime"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT oprema_create(%d, %d, '%s', '%s');""" % (kategorija_id, stanje_id, ime, opis)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##OPREMA UPDATE
@app.route("/uoprema", methods=['GET', 'POST'])
def uoprema():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 1,
        ##          "kategorija_id": 1,
        ##          "stanje_id": 2,
        ##          "ime": "lopata",
        ##          "opis": "za grabljenje listja"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]
        kategorija_id = podatki_json["kategorija_id"]
        stanje_id = podatki_json["stanje_id"]
        ime = podatki_json["ime"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT oprema_update(%s, %s, %s, '%s', '%s');""" % (id, kategorija_id, stanje_id, opis, ime)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##IZPOSOJE CREATE
@app.route("/cizposoje", methods=['GET', 'POST'])
def cizposoje():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "user_id": 1,
        ##          "oprema_id": 2,
        ##          "placnik_id": 2,
        ##          "stanje_id": 2,
        ##          "datum_od": "2020-03-20",
        ##          "datum_do": "2020-03-25",
        ##          "opis": "izposojeno kot ponavadi preko kombija"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        user_id = podatki_json["user_id"]
        oprema_id = podatki_json["oprema_id"]
        placnik_id = podatki_json["placnik_id"]
        stanje_id = podatki_json["stanje_id"]
        datum_od = podatki_json["datum_od"]
        datum_do = podatki_json["datum_do"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT izposoje_create(%d, %d, %d, %d, '%s', '%s', '%s');""" % (user_id, oprema_id, placnik_id, stanje_id, datum_od, datum_do, opis)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##IZPOSOJE UPDATE
@app.route("/uizposoje", methods=['GET', 'POST'])
def uizposoje():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 1,
        ##          "user_id": 1,
        ##          "oprema_id": 2,
        ##          "placnik_id": 2,
        ##          "stanje_id": 2,
        ##          "datum_od": "2020-03-20",
        ##          "datum_do": "2020-03-25",
        ##          "opis": "izposojeno kot ponavadi preko kombija"
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]
        user_id = podatki_json["user_id"]
        oprema_id = podatki_json["oprema_id"]
        placnik_id = podatki_json["placnik_id"]
        stanje_id = podatki_json["stanje_id"]
        datum_od = podatki_json["datum_od"]
        datum_do = podatki_json["datum_do"]
        opis = podatki_json["opis"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT izposoje_update(%d, %d, %d, %d, %d, '%s', '%s', '%s');""" % (id, user_id, oprema_id, placnik_id, stanje_id, datum_od, datum_do, opis)).scalar()
            db.session.commit()
            return jsonify({"bool": r}), 201
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})


##POROCILA CREATE
@app.route("/cporocila", methods=['GET', 'POST'])
def cporocila():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "izposoja_id": 1,
        ##          "porocilo": "Opremo si je izposodil v sredo in jo vrnil v petek. Pri tem oprema ni bila poškodovana..."
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        izposoja_id = podatki_json["izposoja_id"]
        porocilo = podatki_json["porocilo"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT porocilo_create(%d, '%s');""" % (izposoja_id, porocilo)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})

##POROCILA UPDATE
@app.route("/uporocila", methods=['GET', 'POST'])
def uporocila():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 1,
        ##          "izposoja_id": 1,
        ##          "porocilo": "Opremo si je izposodil v sredo in jo vrnil v petek. Pri tem oprema ni bila poškodovana..."
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]
        izposoja_id = podatki_json["izposoja_id"]
        porocilo = podatki_json["porocilo"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT porocilo_update(%d, %d, '%s');""" % (id, izposoja_id, porocilo)).scalar()
            db.session.commit()
            if (r == True):
                return jsonify({"bool": True}), 201
            ##Returned data to program
            else:
                return jsonify({"bool": False})
        except Exception as e:
            print(e)
            return jsonify({"bool": False}), 404
    else:
        return jsonify({"u sent": "nothing"})

##KATEGORIJE DELETE
@app.route("/dkategorije", methods=['GET', 'POST'])
def dkategorije():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM kategorije k WHERE k.id=%d;""" % (id))
            db.session.commit()
            print("true")
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##STANJA DELETE
@app.route("/dstanja", methods=['GET', 'POST'])
def dstanja():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM stanja WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##OPREMA DELETE
@app.route("/doprema", methods=['GET', 'POST'])
def doprema():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM oprema WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##IZPOSOJE DELETE
@app.route("/dizposoje", methods=['GET', 'POST'])
def dizposoje():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM izposoje WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})



##POROČILA DELETE
@app.route("/dporocila", methods=['GET', 'POST'])
def dporocila():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM porocila WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})




##PLACNIKI DELETE
@app.route("/dplacniki", methods=['GET', 'POST'])
def dplacniki():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM placniki WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})




##USER DELETE
@app.route("/duser", methods=['GET', 'POST'])
def duser():
    if (request.method == 'POST'):
        ##example of input data:
        ##        {
        ##          "id": 3,
        ##        }
        podatki_json = request.get_json()
        ##Deviding sent data
        id = podatki_json["id"]

        ##interaction db
        try:
            ##Called function
            db.session.execute("""DELETE FROM users WHERE id=%s;""" % (id))
            db.session.commit()
            return jsonify({"bool": True}), 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


if __name__ == '__main__':
    app.run(debug=True)
