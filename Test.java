public class Test {
    public static void main(String args[]){
        runTests();
    }

    public static void runTests(){
        assert "Just 4".equals(
            new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).toString());

        assert "Just 28".equals(
            new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            }).toString());

        assert "Nothing".equals(
            new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return null;
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            }).toString());

        assert "Right blah".equals(Either.right("blah"));
        assert "Left 5".equals(Either.left(new Integer(5)));
        assert new Integer(6).compareTo(
                Either.right(new Integer(2))
                .fmap(new Function<Integer, Integer>(){
                    public Integer apply(Integer x){
                        return new Integer(x.intValue() * 3);
                    }
                }).right) == 0;

        System.out.println("Assertions passed");
    }
}
