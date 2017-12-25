import { Api } from "./Site.Api";

export class Logic {
    render() {
        let api = new Api();
        api.loadQuestions(); 
    }
    handleEvent() {
        let $questionTypes = $("[name=QuestionType]");
        let $answerPanel = $("#AnswerPanel");

        $answerPanel.show();
        $(".checkbox-choice").hide();
        $(".radio-choice").show();

        $questionTypes.change(function () {
            $("[name=CorrectAnswerIndexes]").removeAttr("checked");

            if ($questionTypes.filter("[value=Text]:checked").length > 0) {
                $answerPanel.hide();
            }
            else if ($questionTypes.filter("[value=Selection]:checked").length > 0) {
                $answerPanel.show();
                $(".checkbox-choice").hide();
                $(".radio-choice").show();
            }
            else {
                $answerPanel.show();
                $(".checkbox-choice").show();
                $(".radio-choice").hide();
            }
        });
    }
}
