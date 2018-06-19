var express = require("express");
var router = express.Router();
var mysql = require('mysql');
var dbConfig = require('../DB/dbConfig');
var sqlCommands = require('../DB/usersql');

var pool = mysql.createPool(dbConfig.mysql);

router.all('/', function (req, res, next) {
    var number = req.body.number;
    var password = req.body.password;

    pool.getConnection( function(err,connection) {

        connection.query(sqlCommands.RankSQL.getTopTen,  null,
            function(err, result) {
                if(result) {
                    var resp_json = []
                    for(var i in result) {
                        resp_json[i] = {
                            "score" : result[i].bestScore
                        }
                    }
                    res.json({
                        "topTen": resp_json 
                    })
                }
                else{
                    res.json({
                        "result_status": 0
                    })
                }
            })
    })
})
module.exports = router;
