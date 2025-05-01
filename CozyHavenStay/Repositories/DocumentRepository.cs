using CozyHavenStay.Contexts;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Models;

namespace CozyHavenStay.Repositories
{
    public class DocumentRepository : Repository<int, Document>
    {
        public DocumentRepository(CozyHavenStayContext context) : base(context)
        {
        }

        public override async Task<Document> GetById(int id)
        {
            var document = await _context.Documents
                .Include(d => d.Customer)  // Ensure we include Customer information as well
                .SingleOrDefaultAsync(d => d.DocumentId == id);

            if (document == null)
                throw new Exception($"Document with ID {id} not found");

            return document;
        }

        public async override Task<IEnumerable<Document>> GetAll()
        {
            var documents = _context.Documents.Include(d => d.Customer);  // Include related Customer data

            if (await documents.CountAsync() == 0)  // Check if there are any documents in the database
                throw new Exception("No documents found");

            return await documents.ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsByCustomerId(int customerId)
        {
            var documents = _context.Documents
                .Where(d => d.CustomerId == customerId)
                .Include(d => d.Customer)  // Optionally include customer details
                .ToListAsync();

            return await documents;
        }

        // You can add more methods here for other custom queries if needed
    }
}
