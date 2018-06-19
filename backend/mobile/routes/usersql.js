var UserSQL = {
	Insert: 'INSERT INTO userInfo(phoneNum, password) VALUES(?, ?)',
	CheckNum: 'SELECT * FROM userInfo where phoneNum = ?',
	QueryAll: 'SELECT * FROM userInfo',
	Login: 'SELECT * FROM userInfo where phoneNum = ?, password = ?'
};

var RankSQL = {
	CheckUser: 'SELECT * FROM userBest where phoneNum = ?',
	UpdateScore: 'UPDATE userBest set bestScore = ? where phoneNum = ?',
	getTopTen: 'SELECT * FROM userBest ORDER BY bestScore DESC LIMIT 10'
};

module.exports = {
	UserSQL: UserSQL,
	RankSQL: RankSQL
};
