# folosirea unei imagini de python ca imagine parinte
FROM python:3.9-slim

# setam directorul de lucru
WORKDIR /app

# copiem fisierul curent in diectorul de lucru
COPY . /app

# instalarea dependintelor aflate in fisierul requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# expunerea portului 5000
EXPOSE 5000

# variabila de mediu cu numele aplicatiei
ENV FLASK_APP=app.py

# rulearea app.py in momentul pornirii
CMD ["flask", "run", "--host=0.0.0.0"]
