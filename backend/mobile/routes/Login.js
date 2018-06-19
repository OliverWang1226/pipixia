var express = require("express");
var router = express.Router();
var mysql = require('mysql');
var dbConfig = require('../DB/dbConfig');
var sqlCommands = require('../DB/usersql');

var pool = mysql.createPool(dbConfig.mysql);


router.all('/', function (req, res, next) {
    var number = req.body.number;
    var password = req.body.password;

    pool.getConnection(function (err, connection) {

        connection.query(sqlCommands.UserSQL.Login,
            [number, password],
            function (err, result1) {
                 //login failed
                 if(result1[0].num == 0) {
                    res.json({
                        'result_status': 0
                    })
                }
                //login success
                else{
                    var rank = -1;
                    var score = 0;
                    connection.query(sqlCommands.RankSQL.getTopTen, null, function (err, result2) {
                        //result为 top 10
                        if (result2) {
                            rrr = result2;
                            for (var i in result2) {
                                if (result2[i].phoneNum == number) {
                                    rank = parseInt(i) + 1;
                                }
                            }
                            connection.query(sqlCommands.RankSQL.CheckUser,
                                number,
                                function (err, result3) {
                                    if (result3) {
                                        score = result3[0].bestScore;
                                    }
				    
                                    res.json({
                                        "result_status": 1,
                                        "curr_rank": rank,
                                        "best_score": score
                                    })
                                })
                        }
                        else {
                            res.json({
                                "result_status": 1,
                                "curr_rank": rank,
                                "best_score": score
                            })
                        }

                    })

                }
               
            })
    })
})
module.exports = router;
