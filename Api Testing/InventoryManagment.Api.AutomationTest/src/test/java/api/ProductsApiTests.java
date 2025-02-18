package api;

import org.testng.annotations.Test;

public class ProductsApiTests {

    @BeforeClass
    public void setup() {
        user = UserFactory.getRandomUser();
        RestAssured.baseURI = Environment.HEROKU;
    }
    @Test
    public void addProductTest() {
        Response response = UserApi.addUser(user);

        response.then().assertThat().statusCode(201);
    }
}
