from flask import Flask, jsonify, request, send_file, send_from_directory
import json
import hashlib
from flask_cors import CORS
import os
import zipfile

app = Flask(__name__)

from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate

app = Flask(__name__, static_url_path='')

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
            db.session.execute("""SELECT create_user('%s', '%s', '%s', '%s', '%s');""" % (ime, priimek, email, geslo_h, tel))
            db.session.commit()
            ##Returned data to program
            return jsonify({'bool': True}), 201
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
            db.session.execute("""SELECT create_placnik('%s', '%s', '%s', '%s', '%s');""" % (ime, priimek, email, geslo_h, tel))
            db.session.commit()
            ##Returned data to program
            return jsonify({'bool': True}), 201
        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'type': "cant use GET"})


##LOGIN
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
        email = podatki_json["email"]

        ##interaction db
        try:
            ##Called function
            r = db.session.execute("""SELECT * FROM users WHERE id=%s LIMIT 1;""" % (email)).first()
            db.session.commit()
            r = str(r)[1:-1]
            r = r.replace(" ", "")
            r = r.replace("'", "")
            r = r.split(",")
            r1 = {"id": int(r[0]), "ime": "%s" % (r[1]), "priimek": "%s" % (r[2]), "email": "%s" % (r[3]),
                  "geslo": "%s" % (r[4]), "tel": "%s"%(r[5])}
            return r1, 200

        except Exception as e:
            print(e)
            return jsonify({'bool': False}), 404
    else:
        return jsonify({'u sent': "nothing"})


# ##PLAYLISTS
# @app.route("/playlists", methods=['GET', 'POST'])
# def playlists():
#     if (request.method == 'POST'):
#         ##example of input data:
#         ##        {
#         ##          "email": "luka1.lah@gmail.ocm",
#         ##        }
#         podatki_json = request.get_json()
#         ##Deviding sent data
#         email = podatki_json["email"]
#
#         ##interaction db
#         try:
#             ##Called function
#             r = db.session.execute(
#                 """SELECT * FROM playlists WHERE user_id=(SELECT id FROM users WHERE email='%s');""" % (
#                     email)).fetchall()
#             db.session.commit()
#             # r=str(r)[1:-1]
#             r2 = {"playlists": []}
#             data = r2.get("playlists")
#             count = 0
#             for i in range(0, len(r)):
#                 r1 = {"id": int(r[i][0]), "user_id": int(r[i][1]), "name": "%s" % (r[i][2]), "url": "%s" % (r[i][3]),
#                       "opis": "%s" % (r[i][4])}
#                 data.append(r1)
#                 count += 1
#             r2["count"] = count
#             return r2, 200
#
#         except Exception as e:
#             print(e)
#             return jsonify({'bool': False}), 404
#     else:
#         return jsonify({'u sent': "nothing"})
#
#
# ##SONGS
# @app.route("/songs", methods=['GET', 'POST'])
# def songs():
#     if (request.method == 'POST'):
#         ##example of input data:
#         ##        {
#         ##          "id": 1
#         ##        }
#         podatki_json = request.get_json()
#         ##Deviding sent data
#         id = podatki_json["id"]
#         print(id)
#
#         ##interaction db
#         try:
#             ##Called function
#             r = db.session.execute("""SELECT * FROM songs WHERE playlist_id=%s;""" % (id)).fetchall()
#             db.session.commit()
#             print(r)
#             # r=str(r)[1:-1]
#             r2 = {"playlists": []}
#             data = r2.get("playlists")
#             count = 0
#             for i in range(0, len(r)):
#                 r1 = {"id": int(r[i][0]), "playlist_id": int(r[i][1]), "ime": "%s" % (r[i][2]),
#                       "izvajalec": "%s" % (r[i][3])}
#                 data.append(r1)
#                 count += 1
#             r2["count"] = count
#             print(r2)
#             return r2, 200
#
#         except Exception as e:
#             print(e)
#             return jsonify({'bool': False}), 404
#     else:
#         return jsonify({'u sent': "nothing"})


##DOWNLOAD
@app.route('/Songs/<path:path>')
def send_js(path):
    return send_from_directory('excel', path)


if __name__ == '__main__':
    app.run(debug=True)
