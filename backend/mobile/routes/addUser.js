var express = require('express');
var router = express.Router();
var mysql = require('mysql');
var dbConfig = require('../DB/dbConfig');
var sqlCommands = require('../DB/usersql');

var pool = mysql.createPool(dbConfig.mysql);

router.post('/', function (req, res, next) {
	var number = req.body.phone_number;
	var password = req.body.password;

	pool.getConnection(function (err, connection) {
		// check wheather this number is exist
		connection.query(sqlCommands.UserSQL.CheckNum,
			number, 
			function (err, result1) {
				// is this number isn't occupied
				if (result1[0].num == 0) {
						connection.query(sqlCommands.UserSQL.Insert,
							[number, password],
							function (err, result2) {
								if (result2) {
									res.json({
										"result_status": 1
									})
								}
								else {
									res.json({
										"result_status": -1
									})
								}
							}
						)//end sqlcommand2
					
				}
				else res.json({
					"result_status": 0
				})
			}
		)//end sqlcommand1
	})//end sql c
})


module.exports = router;

