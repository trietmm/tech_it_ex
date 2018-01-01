var Site =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports) {

	"use strict";
	
	Object.defineProperty(exports, "__esModule", {
	    value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	var Logic = exports.Logic = function () {
	    function Logic() {
	        _classCallCheck(this, Logic);
	
	        this.currentQuestionIndex = 0;
	        this.remainingTime = 0;
	    }
	
	    //
	    // Used to handle events in Test screen
	    //
	
	
	    _createClass(Logic, [{
	        key: "handleQuestionTestEvent",
	        value: function handleQuestionTestEvent(setting) {
	
	            var totalTime = setting.TotalTime;
	            var questionNumber = setting.QuestionNumber;
	            var startedTime = setting.StartedTime;
	            this.currentQuestionIndex = setting.QuestionIndex;
	
	            var startedDateTime = new Date(startedTime);
	            this.remainingTime = totalTime * 60 - (new Date() - startedDateTime) / 1000;
	
	            //Cached dom
	            var $allQuestions = $(".question");
	            var $buttonLeft = $("#ButtonLeft");
	            var $buttonRight = $("#ButtonRight");
	            var $buttonFinish = $("#ButtonFinish");
	            var $timeRemaining = $("#TimeRemaining");
	            var $questionNumber = $("#QuestionNumber");
	            var $buttonPanel = $(".button-panel");
	
	            var questionCount = parseInt($("#QuestionCount").val());
	
	            var _this = this;
	            //Method for displaying question
	            var changeQuestion = function changeQuestion() {
	                $allQuestions.hide();
	                $allQuestions.filter("#Question" + _this.currentQuestionIndex).show();
	                $questionNumber.html(_this.currentQuestionIndex + 1 + "/" + questionCount);
	                if (_this.currentQuestionIndex == questionCount - 1) {
	                    $buttonPanel.addClass("end");
	                } else {
	                    $buttonPanel.removeClass("end");
	                }
	            };
	
	            //Save answer
	            var saveAnswer = function saveAnswer(goToQuestionIndex) {
	                var $currentQuestion = $allQuestions.filter("#Question" + _this.currentQuestionIndex);
	                var questionId = $currentQuestion.find(".question-id").val();
	                var questionType = $currentQuestion.find(".question-type").val();
	                var $inputs = $currentQuestion.find(".question-answer");
	                var value = "";
	
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
	                            value = $inputs.filter(":checked").map(function () {
	                                return this.value;
	                            }).get().join(",");
	                        }
	
	                var saveObj = {
	                    value: value,
	                    questionId: questionId,
	                    goToQuestionIndex: goToQuestionIndex
	                };
	
	                //Save            
	                $.ajax({
	                    type: "POST",
	                    url: "/Home/SaveAnswer",
	                    data: saveObj,
	                    success: function success(data) {
	
	                        //If the test is done, we redirect to Finish page
	                        if (data == "done") {
	                            location = "Finish";
	                        }
	                    }
	                });
	            };
	
	            //Next function
	            var nextQuestion = function nextQuestion() {
	                saveAnswer(_this.currentQuestionIndex + 1);
	                if (_this.currentQuestionIndex < questionCount - 1) {
	                    _this.currentQuestionIndex++;
	                    changeQuestion();
	                }
	            };
	
	            //Prev function
	            var prevQuestion = function prevQuestion() {
	                saveAnswer(_this.currentQuestionIndex - 1);
	                if (_this.currentQuestionIndex > 0) {
	                    _this.currentQuestionIndex--;
	                    changeQuestion();
	                }
	            };
	
	            //Time
	            var timer = null;
	            var updateTime = function updateTime() {
	                $timeRemaining.html("Time remaining: <b>\n                " + ((_this.remainingTime / 60).toFixed(0) + "").padStart(2, 0) + ":\n                " + ((_this.remainingTime % 60).toFixed(0) + "").padStart(2, 0) + "</b>");
	
	                if (_this.remainingTime > 0) _this.remainingTime--;else {
	                    clearInterval(timer);
	                    $timeRemaining.html("<span style='font-weight:bold; color: red'>Time is up!</span>");
	                    $buttonLeft.unbind("click");
	                    $buttonRight.unbind("click");
	                    $buttonFinish.unbind("click");
	                }
	            };
	
	            //Initialize event
	            $buttonLeft.click(prevQuestion);
	            $buttonRight.click(nextQuestion);
	            $buttonFinish.click(nextQuestion);
	            timer = setInterval(updateTime, 1000);
	            changeQuestion();
	            updateTime();
	        }
	
	        //
	        // Used to handle create/update question in admin
	        //
	
	    }, {
	        key: "handleQuestionUpdateEvent",
	        value: function handleQuestionUpdateEvent() {
	
	            //Cached query for refer later
	            var $questionTypes = $("[name=QuestionType]");
	            var $answerPanel = $("#AnswerPanel");
	            var $answerTextPanel = $("#AnswerTextPanel");
	            var $answerText = $answerTextPanel.find("input");
	            var $checkboxChoice = $(".checkbox-choice");
	            var $radioChoice = $(".radio-choice");
	            var $correctAnswerIndexes = $("[name=CorrectAnswerIndexes]");
	
	            $answerPanel.show();
	            $answerTextPanel.hide();
	            $radioChoice.show();
	            $checkboxChoice.hide();
	
	            var changeState = function changeState() {
	                var isInit = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : false;
	
	
	                //Case text, we will hide checkbox or radio answer
	                if ($questionTypes.filter("[value=Text]:checked").length > 0) {
	                    $answerPanel.hide();
	                    $answerTextPanel.show();
	                }
	
	                //Case Selection, hide checkbox, show radio
	                else if ($questionTypes.filter("[value=Selection]:checked").length > 0) {
	                        $answerPanel.show();
	                        $answerTextPanel.hide();
	                        $checkboxChoice.hide();
	                        $radioChoice.show();
	                        $answerText.val("");
	                    }
	                    //Case Multi-Selection, show checkbox, hide radio
	                    else {
	                            $answerPanel.show();
	                            $answerTextPanel.hide();
	                            $checkboxChoice.show();
	                            $radioChoice.hide();
	                            $answerText.val("");
	                        }
	
	                //When we select other question types, we uncheck all previous answer selection
	                $correctAnswerIndexes.filter(":hidden").removeAttr("checked");
	            };
	
	            //Call the method in intial state
	            changeState(true);
	
	            //Set change event for selectbox QuestionTypes
	            $questionTypes.change(changeState);
	        }
	    }]);

	    return Logic;
	}();

/***/ })
/******/ ]);
//# sourceMappingURL=_all.js.map