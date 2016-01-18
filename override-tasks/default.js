var gulp = require('gulp');
var gutil = require('gulp-util');
var runSequence = require('run-sequence');

gulp.task('default', function(cb) {
    runSequence('purge', 'git-submodules', 'build', function(err) { //, 'cover-dotnet'
        if (err) {
            gutil.log(gutil.colors.red(gutil.colors.bold(err)));
        }
        cb();
    });
});

