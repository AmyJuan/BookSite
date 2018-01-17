const webpack = require('webpack');
var path = require('path');
module.exports = {
    entry: {
        "app.js": "./Views/app.ts",

    },
    output: {
        filename: '[name]',
        path: path.resolve(__dirname, 'build')
    },
    devtool: 'eval-source-map',
    //plugins: [
    //     new webpack.LoaderOptionsPlugin({
    //         options: {
    //             htmlLoader: {
    //                 ignoreCustomFragments: [/\{\{.*?}}/],
    //                 minimize: true,
    //                 removeComments: true,
    //                 collapseWhitespace: true
    //             },
    //         }
    //     })
    //],
    module: {
        rules: [
            //{
            //    test: /\.html$/,
            //    exclude: /node_modules/,
            //    loader: "html-loader?minimize=true&interpolate=require"
            //},
            {
                test: /\.ts(x?)$/,
                exclude: /node_modules/,
                loader: 'ts-loader'
            }
        ]
    },
    watch: false,
    watchOptions: {
        ignored: /node_modules/
    },
    externals: {
        "angular": "angualr",
        "jquery": "$",
        "tether": "Tether",
    }
};