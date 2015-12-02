all: Monad.class Maybe.class

Maybe.class: Monad.class Maybe.java
	javac Maybe.java

Monad.class: Monad.java Functor.class
	javac Monad.java

Functor.class: Functor.java Function.class
	javac Functor.java

Function.class: Function.java
	javac Function.java

clean:
	rm *.class
