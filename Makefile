all: Test.class

run: Test.class
	java Test

Test.class: Test.java Maybe.class
	javac Test.java

Maybe.class: Functor.class Maybe.java
	javac Maybe.java

Monad.class: Monad.java Functor.class
	javac Monad.java

Functor.class: Functor.java Function.class
	javac Functor.java

Function.class: Function.java
	javac Function.java

clean:
	rm *.class
