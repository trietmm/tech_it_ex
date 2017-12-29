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
/***/ (function(module, exports, __webpack_require__) {

	__webpack_require__(1);
	module.exports = __webpack_require__(2);


/***/ }),
/* 1 */
/***/ (function(module, exports) {

	"use strict";
	
	Object.defineProperty(exports, "__esModule", {
	    value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	var Api = exports.Api = function () {
	    function Api() {
	        _classCallCheck(this, Api);
	    }
	
	    _createClass(Api, [{
	        key: "loadQuestions",
	        value: function loadQuestions() {
	            console.log("load questions!");
	        }
	    }]);

	    return Api;
	}();

/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

	"use strict";
	
	Object.defineProperty(exports, "__esModule", {
	    value: true
	});
	exports.Logic = undefined;
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _Site = __webpack_require__(1);
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	var Logic = exports.Logic = function () {
	    function Logic() {
	        _classCallCheck(this, Logic);
	    }
	
	    _createClass(Logic, [{
	        key: "render",
	        value: function render() {
	            var api = new _Site.Api();
	            api.loadQuestions();
	        }
	    }, {
	        key: "handleEvent",
	        value: function handleEvent() {
	            var $questionTypes = $("[name=QuestionType]");
	            var $answerPanel = $("#AnswerPanel");
	
	            $answerPanel.show();
	            $(".checkbox-choice").hide();
	            $(".radio-choice").show();
	
	            var changeState = function changeState() {
	                var isInit = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : false;
	
	
	                if ($questionTypes.filter("[value=Text]:checked").length > 0) {
	                    $answerPanel.hide();
	                } else if ($questionTypes.filter("[value=Selection]:checked").length > 0) {
	                    $answerPanel.show();
	                    $(".checkbox-choice").hide();
	                    $(".radio-choice").show();
	                } else {
	                    $answerPanel.show();
	                    $(".checkbox-choice").show();
	                    $(".radio-choice").hide();
	                }
	                $("[name=CorrectAnswerIndexes]:hidden").removeAttr("checked");
	            };
	
	            changeState(true);
	            $questionTypes.change(changeState);
	        }
	    }]);

	    return Logic;
	}();

/***/ })
/******/ ]);
//# sourceMappingURL=_all.js.map