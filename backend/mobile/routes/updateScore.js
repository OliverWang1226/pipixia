var express = require('express');
var router = express.Router();
var mysql = require('mysql');
var dbConfig = require('../DB/dbConfig');
var sqlCommands = require('../DB/usersql');

var pool = mysql.createPool(dbConfig.mysql);

router.all('/', function (req, res, next) {
    var number = req.body.phone_number;
    var new_score = req.body.curr_score;
    var old_score = 0;
    var best_score;
    var rank = -1;

    pool.getConnection(function (err, connection) {
        //get old score
        connection.query(sqlCommands.RankSQL.CheckUser, number, function (err, result1) { 
            if (result1) {
                console.log("get old score: " +result1[0].bestScore);
                old_score =result1[0].bestScore;
                //if new score is bigger
	        if (new_score > old_score) {
                    //update score and get rank
                    best_score = new_score
                    connection.query(sqlCommands.RankSQL.UpdateScore,
                        [new_score, number],
                        function (err, result2) {
                            //get rank
                            connection.query(sqlCommands.RankSQL.getTopTen, null, function (err, result3) {
                                if (result3) {
                                    for (var i in result3) {
                                        if (result3[i].phoneNum == number) {
                                            rank = parseInt(i) + 1;
                                        }
                                    }
                                }
                                res.json({
                                    "curr_rank": rank,
                                    "best_score": best_score
                                })
                            })
                        })
                }
		//if old score is bigger
                else {
			console.log("old score is bigger than new");
                    best_score = old_score
                    //get rank
                    connection.query(sqlCommands.RankSQL.getTopTen, null, function (err, result4) {
                        if (result4) {
                            for (var i in result4) {
                                if (result4[i].phoneNum == number) {
                                    rank = parseInt(i) + 1;
                                }
                            }
                        }
                        res.json({
                            "curr_rank": rank,
                            "best_score": best_score
                        })
                    })
                }
            }
            else {
                //new score is bigge
                console.log("you have no score in DB");
                best_score = new_score
                //insert into userBest
                connection.query(sqlCommands.RankSQL.Insert, [number, best_score], function (err, result5) {
                    connection.query(sqlCommands.RankSQL.getTopTen, null, function(err, result6) {
                        if(result6) {
                            for (var i in result6) {
                                if (result6[i].phoneNum == number) {
                                    rank = parseInt(i) + 1;
                                }
                            }
                        }
                        res.json({
                            "curr_rank": rank,
                            "best_score": best_score
                        })
                    })
                })
            }
        })
    })
})
module.exports = router;
