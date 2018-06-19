var UserSQL = {
	Insert: 'INSERT INTO userInfo VALUES(?, ?)',
	CheckNum: 'SELECT count(*) as num FROM userInfo where phoneNum = ?',
	QueryAll: 'SELECT * FROM userInfo',
	Login: 'SELECT count(*) as num FROM userInfo where phoneNum = ? and password = ?'
};

var RankSQL = {
	Insert: "INSERT INTO userBest(phoneNum, bestScore) VALUES(?, ?)",
	CheckUser: 'SELECT * FROM userBest where phoneNum = ?',
	UpdateScore: 'UPDATE userBest set bestScore = ? where phoneNum = ?',
	getTopTen: 'SELECT * FROM userBest ORDER BY bestScore DESC LIMIT 10'
};

module.exports = {
	UserSQL: UserSQL,
	RankSQL: RankSQL
};
