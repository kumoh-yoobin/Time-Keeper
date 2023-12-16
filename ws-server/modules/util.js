module.exports = {
  getToday : () => {
    const date = new Date();
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}T${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  },

  errhandler : (method, err) => {
    if (err) {
      console.log(`[${module.exports.getToday()}][ERROR][${method}] : ${err}`) 
      return true;
    }
    return false;
  },

  createCards : () =>{
    // 0부터 7까지의 숫자를 각각 두 번씩 배열에 추가
    let array = [];
    for (let i = 0; i < 10; i++) {
        array.push(i, i);
    }

    // 배열을 랜덤하게 섞기 (Fisher-Yates shuffle 알고리즘)
    for (let i = array.length - 1; i > 0; i--) {
        let j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]]; // ES6 구조 분해 할당을 사용한 요소 교환
    }

    return array;
  }
}