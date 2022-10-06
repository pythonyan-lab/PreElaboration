# Funzioni di lettura ed eliminazione documenti da collection Mongodb
Il prerequisito nasce dal fatto che nella collection "PreElaboration" di CBAS  essendoci documenti duplicati è stato deciso di utilizzare il seguente meccanismo:
- Creazione collection contenente i  record duplicati, usare con Shell Mongo:
  ```
  db.getCollection('pre_elaboration').aggregate([{"$group" : { "_id": "$review_id", "count": { "$sum": 1 }}},{"$match": {"Id" :{"$ne" : null } , "count" : {"$gt": 1} } }, 
    {"$project": {"name" : "$_id", "_id" : 0} }, { $out : "duplicate_reviews" } ])
  ```
-  ciclo che itera sulla precedente collection per raccogliere l'*"objectId"* utilizzando il campo *"review_id"* ed il filtro che include i documenti che hanno il campo *"processed"* vuoto. 
-  ciclo che itera sulla collection *pre_elaboration* ed ==elimina== il documento corrispondete al parametro "*object_Id*" passato come filtro.

- [x] Crea collection
- [x] Leggi collection
- [x] Elimina record

Molto divertente! :joy: 