/// <binding ProjectOpened='watchall' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    sourcemaps = require('gulp-sourcemaps'),
    gutil = require('gulp-util'),
    babel = require('gulp-babel'),
    webpack = require('gulp-webpack'),
    rename = require('gulp-rename');

var paths = {
    webroot: "./"
};

paths.jsSite = paths.webroot + "Scripts/site/**/[^_]*.js";
paths.js = paths.webroot + "Scripts/js/";
paths.minJs = paths.webroot + "Scripts/js/**/*.min.js";
paths.css = paths.webroot + "Content/css/**/[^_]*.css";
paths.minCss = paths.webroot + "Content/css/**/*.min.css";
paths.scss = paths.webroot + "Content/scss/**/[^_]*.scss";
paths.scssDest = paths.webroot + "Content/css";
paths.concatJsDest = paths.webroot + "Scripts/js/site.min.js";
paths.concatCssDest = paths.webroot + "Content/css/site.min.css";


gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});


gulp.task("min:css", function () {
    gulp.src([paths.css, "!" + paths.minCss])
        .pipe(cssmin())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest(paths.webroot + "/css"));
});

gulp.task('transpile:js', function () {
    return gulp.src([paths.jsSite])
        //.pipe(sourcemaps.init())
        //.pipe(babel())
        //.pipe(sourcemaps.write("."))
        //.pipe(gulp.dest(paths.js));

        .pipe(webpack({
            module: {
                loaders: [{
                    test: /\.js$/,
                    loader: 'babel-loader',
                    exclude: /node_modules/,
                    query: {
                        presets: ['es2015']
                    }
                }]
            },
            output: {
                filename: '_all.js',
                libraryTarget: 'var',
                library: 'Site'
            },
            devtool: "source-map"
        }))
        //.pipe(sourcemaps.write("."))
        .pipe(gulp.dest(paths.js));
});

gulp.task("sass", function () {
    gulp.src(paths.scss)
        .pipe(sourcemaps.init())
        .pipe(sass())
        .on('error', function (err) {
            gutil.log(err.message);
            this.emit('end');
        })
        .pipe(sourcemaps.write("."))
        .pipe(gulp.dest(paths.scssDest));
});


gulp.task('watchall', function () {
    gulp.watch([paths.scss], ['sass']);
    gulp.watch([paths.jsSite], ['transpile:js']);
});
