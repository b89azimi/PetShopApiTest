
Summary of Testing Approach

    Test Scenarios: We crafted a variety of positive and negative scenarios for the Pet, Store, and User resources based on the Swagger documentation. These scenarios cover:
        Pet API: Creating, retrieving, updating, and deleting pets.
        Store API: Placing and retrieving orders and deleting them by ID.
        User API: User creation, retrieval, updating, and deletion.

    Testing Categories:
        Integration Tests: To validate each API endpoint's functionality independently.
        ORM Tests: Verifying data storage and retrieval using an in-memory database, testing models like Order, Pet, and User.
        End-to-End Tests: Full workflow tests simulating real user interactions across services to ensure all components work together.

    End-to-End Test Example: A lifecycle test is included to verify key workflows, such as creating, retrieving, updating, and deleting a pet. Using NUnit and RestSharp, this test confirms data accuracy and system behavior across all steps in the pet lifecycle.

    Automation:
        The testing suite is automated with NUnit and RestSharp, ensuring efficient validation of CRUD operations.
        Tests can run in CI/CD pipelines and are configured for flexibility across environments.

    Environment & Dependencies:
        Environment settings are managed in configuration files (e.g., Urls.BaseUrl).
        Schema validation checks are included to ensure data integrity in responses.
        Tests are designed for seamless execution in Windows environments via CLI or IDEs.
