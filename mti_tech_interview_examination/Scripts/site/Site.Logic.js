import { Api } from "./Site.Api";

export class Logic {
    constructor() {
        this.currentQuestionIndex = 0;
        this.remainingTime = 0;
        this.api = new Api();
    }
    
    render() {
        let api = new Api();
        api.loadQuestions(); 
    }
    //
    // Used to handle events in Test screen
    //
    handleQuestionTestEvent(setting) {

        let totalTime = setting.TotalTime;
        let questionNumber = setting.QuestionNumber;
        let startedTime = setting.StartedTime;
        this.currentQuestionIndex = setting.QuestionIndex;

        let startedDateTime = new Date(startedTime);
        this.remainingTime = totalTime * 60 - (new Date() - startedDateTime) / 1000;

        //Cached dom
        let $allQuestions = $(".question");
        let $buttonLeft = $("#ButtonLeft");
        let $buttonRight = $("#ButtonRight");
        let $timeRemaining = $("#TimeRemaining");
        let $questionNumber = $("#QuestionNumber");

        let questionCount = parseInt($("#QuestionCount").val());
        

        let _this = this;
        //Method for displaying question
        let changeQuestion = function () {
            $allQuestions.hide();
            $allQuestions.filter("#Question" + _this.currentQuestionIndex).show();
            $questionNumber.html((_this.currentQuestionIndex + 1) + "/" + questionCount);
            if (_this.currentQuestionIndex == questionCount - 1) {
                //Change right button to Finish button
                $buttonRight.html("Finish");
            }
            else {
                $buttonRight.html("<span class='glyphicon glyphicon-chevron-right'></span>");
            }
        };

        //Save answer
        let saveAnswer = function (goToQuestionIndex) {
            let $currentQuestion = $allQuestions.filter("#Question" + _this.currentQuestionIndex);
            let questionId = $currentQuestion.find(".question-id").val();
            let questionType = $currentQuestion.find(".question-type").val();
            let $inputs = $currentQuestion.find(".question-answer");
            let value = "";

            //Case Text 
            if (questionType == "Text") {
                value = $inputs.val();
            }

            //Case selection
            else if (questionType == "Selection") {
                value = $inputs.filter(":checked").val();
            }

            //Case Multi-selection
            else {
                value = $inputs.filter(":checked").map(function () { return this.value; }).get().join(",");
            }

            let saveObj = {
                value: value,
                questionId: questionId,
                goToQuestionIndex: goToQuestionIndex
            };

            //Save
            _this.api.saveAnswer(saveObj);
        };

        //Next function
        let nextQuestion = function () {
            saveAnswer(_this.currentQuestionIndex + 1);
            if (_this.currentQuestionIndex < questionCount - 1) {
                _this.currentQuestionIndex++;
                changeQuestion();
            }
        };

        //Prev function
        let prevQuestion = function () {
            saveAnswer(_this.currentQuestionIndex - 1);
            if (_this.currentQuestionIndex > 0) {
                _this.currentQuestionIndex--;
                changeQuestion();
            }
        };

        //Time
        let timer = null;
        let updateTime = function () {
            $timeRemaining.html(`Time remaining: <b>
                ${((_this.remainingTime / 60).toFixed(0) + "").padStart(2, 0)}:
                ${((_this.remainingTime % 60).toFixed(0) + "").padStart(2, 0)}</b>`);

            if (_this.remainingTime > 0)
                _this.remainingTime--;
            else {
                clearInterval(timer);
                $timeRemaining.html(`<span style='font-weight:bold; color: red'>Time is up!</span>`);
                $buttonLeft.unbind("click");
                $buttonRight.unbind("click");
            }
        }

        //Initialize event
        $buttonLeft.click(prevQuestion);
        $buttonRight.click(nextQuestion);
        timer = setInterval(updateTime, 1000);
        changeQuestion();
        updateTime();
    }

    //
    // Used to handle create/update question in admin
    //
    handleQuestionUpdateEvent() {

        //Cached query for refer later
        let $questionTypes = $("[name=QuestionType]");
        let $answerPanel = $("#AnswerPanel");
        let $checkboxChoice = $(".checkbox-choice");
        let $radioChoice = $(".radio-choice");
        let $correctAnswerIndexes = $("[name=CorrectAnswerIndexes]");

        $answerPanel.show();        
        $radioChoice.show();
        $checkboxChoice.hide();

        let changeState = function (isInit = false) {

            //Case text, we will hide checkbox or radio answer
            if ($questionTypes.filter("[value=Text]:checked").length > 0) {
                $answerPanel.hide();
            }

            //Case Selection, hide checkbox, show radio
            else if ($questionTypes.filter("[value=Selection]:checked").length > 0) {
                $answerPanel.show();
                $checkboxChoice.hide();
                $radioChoice.show();
            }
            //Case Multi-Selection, show checkbox, hide radio
            else {
                $answerPanel.show();
                $checkboxChoice.show();
                $radioChoice.hide();
            }

            //When we select other question types, we uncheck all previous answer selection
            $correctAnswerIndexes.filter(":hidden").removeAttr("checked");
        };

        //Call the method in intial state
        changeState(true);

        //Set change event for selectbox QuestionTypes
        $questionTypes.change(changeState);
    }


}
