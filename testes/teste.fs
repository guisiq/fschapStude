.Lambda #Lambda1<System.Func`2[System.String,System.Int32]>(System.String $delegateArg0) {
    .Extension<NJection.LambdaConverter.Expressions.MethodDeclaration> {
        .Invoke (.Lambda #Lambda2<System.Func`2[System.String,System.Int32]>)($delegateArg0)
    }
}

.Lambda #Lambda2<System.Func`2[System.String,System.Int32]>(System.String $delegateArg0) {
    .Extension<NJection.LambdaConverter.Expressions.MethodBlock> {
        .Block() {
            .Extension<NJection.LambdaConverter.Expressions.Return> {
                .Return ReturnStatement { .Extension<NJection.LambdaConverter.Expressions.Invocation> {
                    .Call Program.parse(.Extension<NJection.LambdaConverter.Expressions.Identifier> {
                            $delegateArg0
                        })
                } }
            };
            .Label
                .Default(System.Int32)
            .LabelTarget ReturnStatement:
        }
    }
}