export class Api {
    loadQuestions() {
        console.log("load questions!"); 
    }

    saveAnswer(saveObj) {
        $.ajax({
            type: "POST",
            url: "/Home/SaveAnswer",
            data: saveObj,
            success: function (msg) {
                console.log(msg);
            }
        });
    }
}  