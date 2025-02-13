# Ahegel - Patient Management Service

## **Project Description:**

Ahegel is a patient management system that provides an interface for performing CRUD (Create, Read, Update, Delete) operations on patient data. The system supports basic features such as retrieving patient details, creating new patients, updating existing records, and soft deleting patients. The application ensures data consistency and integrity through asynchronous operations, utilizing a service-oriented architecture.

This repository contains the service layer that interacts with patient data, providing a clean abstraction for the data layer. It defines methods that interact with patient entities and enable smooth integration with other parts of the system, including controllers or other external services.

---

### **1. Asynchronous Operations:**
- **Why?**  
  The operations for interacting with the patient data are asynchronous. This decision allows the system to handle potentially long-running I/O operations, such as database interactions, without blocking the main thread. This approach improves scalability and responsiveness in high-traffic environments.

### **2. Use of `IReadOnlyList<T>` for Data Retrieval:**
- **Why?**  
  Instead of returning a mutable collection like `List<Patient>`, `IReadOnlyList<Patient>` is used. This ensures that the data retrieved cannot be modified, promoting immutability and enhancing the integrity of the data exposed by the service layer.

### **3. Soft Deletion Strategy:**
- **Why?**  
  The decision to implement soft deletion (`SoftDeletePatient()`) rather than hard deletion is made to preserve patient records for auditing or historical purposes. Soft deletion marks a record as inactive, maintaining its data in the system without exposing it in active queries. This is often a business requirement for healthcare systems to maintain records for compliance.

### **4. Return Types:**
- **Why?**  
  - **For `CreatePatient()`**, the method returns an integer representing the patient's ID after creation. This allows clients to know exactly which record has been created.
  - **For `UpdatePatient()`**, a `bool` is returned to indicate whether the update was successful. This simplifies error handling and ensures that consumers of the service can easily handle failures without needing to process a full object.

### **5. Explicit Naming of Methods:**
- **Why?**  
  The method names like `GetPatientById()` instead of simply `GetPatient()` are chosen for clarity. It’s important to follow a consistent and descriptive naming convention to enhance readability and avoid ambiguity. This makes the code easier to understand and maintain, especially as the application grows.

---

## **Technologies Used:**
- **C#**  
- **.NET Core**
- **PostgreSQL** as the database management system
- **Entity Framework Core (EF Core)** for data interaction
- **Task-based Asynchronous Pattern (TAP)** for handling asynchronous methods

---

## **Setup Instructions:**

1. Clone the repository:
   ```bash
   git clone https://github.com/Chathurangak90/AhegelAssessment
